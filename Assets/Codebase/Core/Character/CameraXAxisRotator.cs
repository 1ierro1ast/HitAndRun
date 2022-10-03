using UnityEngine;

namespace Codebase.Core.Character
{
    public class CameraXAxisRotator : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        private ICameraTarget _cameraTarget;

        private void Start()
        {
            _cameraTarget = GetComponentInParent<ICameraTarget>();
            if(!_cameraTarget.IsLocalPlayer) _camera.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_cameraTarget.CanMove)
                _camera.transform.localRotation = Quaternion.Euler(_cameraTarget.RotationX, 0, 0);
        }
    }
}