using System;
using System.Collections;
using Codebase.Core.Character.States;
using Codebase.Core.Scores;
using Codebase.Core.Settings;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.AssetManagement;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Character
{
    [RequireComponent(typeof(ScoreCounter))]
    public class CharacterBehaviour : NetworkBehaviour, ITagable
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Renderer _view;
        private CharacterStateMachine _characterStateMachine;
        public bool CanTag => _characterStateMachine.CompareState<ShiftImpulseState>();
        private bool _isTagged;
        private TagSettings _tagSettings;
        private Color _originalColor;

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
            callback();
            _isTagged = true;
            _view.material.color = _tagSettings.TaggedColor;
            StartCoroutine(TaggedTimer());
        }

        private IEnumerator TaggedTimer()
        {
            yield return new WaitForSeconds(_tagSettings.TaggedTime);
            _isTagged = false;
            _view.material.color = _originalColor;
        }
    }
}