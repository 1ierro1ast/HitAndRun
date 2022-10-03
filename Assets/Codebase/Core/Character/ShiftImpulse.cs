using System;
using System.Collections;
using UnityEngine;

namespace Codebase.Core.Character
{
    public class ShiftImpulse : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shift();
            }
        }

        private void Shift()
        {
            var targetPoint = GetShiftTargetPoint();
            StartCoroutine(MoveToPoint(targetPoint, _gameSettings.ShiftImpulseSettings.ShiftDuration,
                _gameSettings.ShiftImpulseSettings.Curve));
        }

        private IEnumerator MoveToPoint(Vector3 target, float duration, AnimationCurve curve)
        {
            _characterController.enabled = false;
            var time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(
                    transform.position,
                    target,
                    curve.Evaluate(time / duration));
                yield return null;
            }

            yield return null;
            transform.position = target;
            _characterController.enabled = true;
        }

        private Vector3 GetShiftTargetPoint()
        {
            return transform.position + transform.forward * _gameSettings.ShiftImpulseSettings.Distance;
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private float _distance;
            [SerializeField] private float _shiftDuration;
            [SerializeField] private AnimationCurve _curve;

            public float Distance => _distance;

            public float ShiftDuration => _shiftDuration;

            public AnimationCurve Curve => _curve;
        }
    }
}