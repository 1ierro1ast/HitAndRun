using System;
using System.Collections;
using UnityEngine;

namespace Codebase
{
    public class ShiftImpulse : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
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

            StartCoroutine(MoveToPoint(targetPoint, 1f, _gameSettings.ShiftImpulseSettings.Curve));
        }

        private IEnumerator MoveToPoint(Vector3 target, float duration, AnimationCurve curve)
        {
            var time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                yield return null;
            }
        }

        private Vector3 GetShiftTargetPoint()
        {
            return transform.forward * _gameSettings.ShiftImpulseSettings.Distance;
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private float _distance;
            [SerializeField] private AnimationCurve _curve;

            public float Distance => _distance;
            public AnimationCurve Curve => _curve;
        }
    }
}