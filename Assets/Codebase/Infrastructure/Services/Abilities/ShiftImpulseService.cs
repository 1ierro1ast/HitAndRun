using System.Collections;
using Codebase.Core.Character;
using Codebase.Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Abilities
{
    public class ShiftImpulseService : IShiftImpulseService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ICoroutineRunner _coroutineRunner;
        private ShiftImpulseSettings _shiftImpulseSettings;

        public ShiftImpulseService(IAssetProvider assetProvider, ICoroutineRunner coroutineRunner)
        {
            _assetProvider = assetProvider;
            _coroutineRunner = coroutineRunner;
            _shiftImpulseSettings = _assetProvider.GetScriptableObject<GameSettings>(AssetPath.GameSettingsPath).ShiftImpulseSettings;
        }

        public void Shift(CharacterController characterController, Transform transform)
        {
            var targetPoint = GetShiftTargetPoint(transform);
            _coroutineRunner.StartCoroutine(MoveToPoint(targetPoint, _shiftImpulseSettings.ShiftDuration,
                _shiftImpulseSettings.Curve, characterController, transform));
        }

        private IEnumerator MoveToPoint(Vector3 target, float duration, AnimationCurve curve,
            CharacterController characterController, Transform transform)
        {
            characterController.enabled = false;
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
            characterController.enabled = true;
        }

        private Vector3 GetShiftTargetPoint(Transform transform)
        {
            return transform.position + transform.forward * _shiftImpulseSettings.Distance;
        }
    }
}