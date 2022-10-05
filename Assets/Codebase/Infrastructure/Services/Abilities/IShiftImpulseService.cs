using UnityEngine;

namespace Codebase.Infrastructure.Services.Abilities
{
    public interface IShiftImpulseService : IService
    {
        public void Shift(CharacterController characterController, Transform transform);
    }
}