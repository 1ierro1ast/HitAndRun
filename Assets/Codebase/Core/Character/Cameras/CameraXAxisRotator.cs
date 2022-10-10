using UnityEngine;

namespace Codebase.Core.Character.Cameras
{
    public class CameraXAxisRotator : MonoBehaviour
    {
        private Camera _camera;
        private ICameraTarget _cameraTarget;

        private void Start()
        {
            _camera = GetComponent<Camera>();
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