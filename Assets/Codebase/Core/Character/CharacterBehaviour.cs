using System.Collections;
using Codebase.Core.Character.States;
using Codebase.Core.Settings;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace Codebase.Core.Character
{
    public class CharacterBehaviour : MonoBehaviour, ITagable
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Renderer _view;
        private CharacterStateMachine _characterStateMachine;
        public bool CanTag => _characterStateMachine.CompareState<ShiftImpulseState>();
        private bool _isTagged = false;
        private TagSettings _tagSettings;
        private Color _originalColor;

        private void Awake()
        {
            _originalColor = _view.material.color;
            _tagSettings = AllServices.Container.Single<IAssetProvider>()
                .GetScriptableObject<GameSettings>(AssetPath.GameSettingsPath).TagSettings;
            _characterStateMachine = new CharacterStateMachine(_characterController, transform, AllServices.Container);
            _characterStateMachine.Enter<RunState>();
        }

        public void Tag()
        {
            if (_isTagged) return;
            Debug.Log("TAGGED");
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