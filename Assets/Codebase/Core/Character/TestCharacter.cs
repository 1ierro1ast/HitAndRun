using System;
using System.Collections;
using Codebase.Core.Settings;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace Codebase.Core.Character
{
    public class TestCharacter : MonoBehaviour, ITagable
    {
        [SerializeField] private Renderer _view;
        public bool CanTag => false;
        public void Tag(Action callback)
        {
            if (_isTagged) return;
            Debug.Log("TAGGED");
            callback();
            _isTagged = true;
            _view.material.color = _tagSettings.TaggedColor;
            StartCoroutine(TaggedTimer());
        }

        private bool _isTagged = false;
        private TagSettings _tagSettings;
        private Color _originalColor;

        private void Awake()
        {
            _originalColor = _view.material.color;
            _tagSettings = AllServices.Container.Single<IAssetProvider>()
                .GetScriptableObject<GameSettings>(AssetPath.GameSettingsPath).TagSettings;
        }

        private IEnumerator TaggedTimer()
        {
            yield return new WaitForSeconds(_tagSettings.TaggedTime);
            _isTagged = false;
            _view.material.color = _originalColor;
        }
    }
}