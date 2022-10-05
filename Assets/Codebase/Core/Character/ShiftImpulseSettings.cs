using System;
using UnityEngine;

namespace Codebase.Core.Character
{
    [Serializable]
    public class ShiftImpulseSettings
    {
        [SerializeField] private float _distance;
        [SerializeField] private float _shiftDuration;
        [SerializeField] private AnimationCurve _curve;

        public float Distance => _distance;

        public float ShiftDuration => _shiftDuration;

        public AnimationCurve Curve => _curve;
    }
}