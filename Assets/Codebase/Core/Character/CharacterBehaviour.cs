using System;
using System.Collections;
using Codebase.Core.Character.States;
using Codebase.Core.Scores;
using Codebase.Core.Settings;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.NameSystem;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Character
{
    [RequireComponent(typeof(ScoreCounter))]
    public class CharacterBehaviour : NetworkBehaviour, ICanBeingTagged, INameHolder
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Renderer _view;

        [SyncVar(hook = nameof(SyncName))] 
        private string _syncName;

        [SyncVar(hook = nameof(SyncCharacterColor))]
        private Color32 _syncColor;

        private CharacterStateMachine _characterStateMachine;
        private TagSettings _tagSettings;
        private INameService _nameService;
        private bool _isTagged;
        private Color32 _originalColor;

        public bool CanTag => _characterStateMachine.CompareState<ShiftImpulseState>();
        public string Name => _syncName;
        
        private void Awake()
        {
            _originalColor = _view.material.color;
            _tagSettings = AllServices.Container.Single<IAssetProvider>()
                .GetScriptableObject<GameSettings>(AssetPath.GameSettingsPath).TagSettings;

            _characterStateMachine = new CharacterStateMachine(
                _characterController, transform, AllServices.Container, this);

            _characterStateMachine.Enter<RunState>();
        }

        public void Tag(Action callback)
        {
            if (_isTagged) return;
            _isTagged = true;
            SetNewColor(_tagSettings.TaggedColor);
            StartCoroutine(TaggedTimer());
            callback();
        }

        private IEnumerator TaggedTimer()
        {
            yield return new WaitForSeconds(_tagSettings.TaggedTime);
            _isTagged = false;
            SetNewColor(_originalColor);
        }

        public override void OnStartClient()
        {
            SetName();
        }

        private void SetName()
        {
            if (!hasAuthority) return;

            _nameService = AllServices.Container.Single<INameService>();
            if (isServer)
            {
                ChangeNameValue(_nameService.PlayerName);
            }
            else
            {
                CmdChangeName(_nameService.PlayerName);
            }
        }

        private void SetNewColor(Color32 color)
        {
            if (isServer)
            {
                ChangeColorValue(color);
            }
            else
            {
                CmdChangeColor(color);
            }
        }

        private void SyncName(string oldValue, string newValue) => Debug.Log($"new name: {newValue}");

        private void SyncCharacterColor(Color32 oldValue, Color32 newValue) => _view.material.color = newValue;

        [Server]
        private void ChangeNameValue(string newValue) => _syncName = newValue;

        [Command]
        private void CmdChangeName(string newValue) => ChangeNameValue(newValue);
        
        [Server]
        private void ChangeColorValue(Color32 newValue) => _syncColor = newValue;

        [Command(requiresAuthority = false)]
        private void CmdChangeColor(Color32 newValue) => ChangeColorValue(newValue);
    }
}