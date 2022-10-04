﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI
{
    public class Popup : MonoBehaviour
    {
        [Header("Open/Close Settings")]
        [SerializeField] private GameObject _body;
        [SerializeField] private float _openDelay;
        [SerializeField] private float _closeDelay;
        [SerializeField] private float _closeAnimDuration;
        [SerializeField] private Animator[] _animators;
        [SerializeField] private Button _closePopupButton;

        public static event Action<bool> PopupActionButtonPressed;

        private const string AnimatorOpenPopupBoolKey = "IsOpen";
        private static readonly int Open = Animator.StringToHash(AnimatorOpenPopupBoolKey);
        private bool _isOpen;

        private Coroutine _openCloseCoroutine; 

        public bool IsOpen => _isOpen;

        private void Awake()
        {
            _isOpen = false;
            _body.SetActive(false);
            _closePopupButton?.onClick.AddListener(OnClosePopupButtonClick);
            OnInitialization();
        }

        protected virtual void OnInitialization() {}
    
        public void OpenPopup()
        {
            _isOpen = true;
            gameObject.SetActive(true);
            
            if (!gameObject.activeInHierarchy)
                return;

            if (_openCloseCoroutine != null)
                StopCoroutine(_openCloseCoroutine);
            OnOpenPopup();
            _openCloseCoroutine = StartCoroutine(OpenCoroutine());
        }

        protected virtual void OnOpenPopup() {}

        public void ClosePopup()
        {
            _isOpen = false;
            
            if (!gameObject.activeInHierarchy)
                return;

            if (_openCloseCoroutine != null)
                StopCoroutine(_openCloseCoroutine);
            OnClosePopup();
            _openCloseCoroutine = StartCoroutine(CloseCoroutine());
        }
    
        private void OnClosePopupButtonClick()
        {
            ClosePopup();
        }

        private IEnumerator CloseCoroutine()
        {
            yield return new WaitForSeconds(_closeDelay);
        
            foreach (var animator in _animators)
            {
                if (animator.gameObject.activeInHierarchy)
                    animator.SetBool(Open, false);
            }
        
            yield return new WaitForSeconds(_closeAnimDuration);
            _body.SetActive(false);
        }
    
        private IEnumerator OpenCoroutine()
        {
            yield return new WaitForSeconds(_openDelay);
        
            foreach (var animator in _animators)
            {if (animator.gameObject.activeInHierarchy)
                    animator.SetBool(Open, true);
            }
            _body.SetActive(true);
        }

        protected virtual void OnClosePopup() {}
    }
}