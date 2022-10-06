using System;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Abilities
{
    public interface IShiftImpulseService : IService
    {
        void Shift(CharacterController characterController, Transform transform, Action onImpulseFinishedCallback);
    }
}