using System;
using UnityEngine;

namespace Codebase.Core.Settings
{
    [Serializable]
    public class CharacterMovementsSettings
    {
        [SerializeField] private float _walkingSpeed = 7.5f;
        [SerializeField] private float _runningSpeed = 11.5f;
        [SerializeField] private float _jumpSpeed = 8.0f;
        [SerializeField] private float _gravity = 20.0f;
        [SerializeField] private float _lookSpeed = 2.0f;
        [SerializeField] private float _lookXLimit = 45.0f;
        
        public float WalkingSpeed => _walkingSpeed;

        public float RunningSpeed => _runningSpeed;

        public float JumpSpeed => _jumpSpeed;

        public float Gravity => _gravity;

        public float LookSpeed => _lookSpeed;

        public float LookXLimit => _lookXLimit;
    }
}
