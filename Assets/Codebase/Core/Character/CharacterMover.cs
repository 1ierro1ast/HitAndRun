using Codebase.Core.Settings;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.Input;
using Codebase.Infrastructure.Services.Spawn;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMover : NetworkBehaviour, ICameraTarget
    {
        private CharacterController _characterController;
        private Vector3 _moveDirection = Vector3.zero;
        private float _rotationX = 0;
        private bool _canMove = true;
        private Vector3 _currentForward;
        private Vector3 _currentRight;
        private float _currentSpeedX;
        private float _currentSpeedY;
        private float _movementDirectionY;
        private bool _isRunning;

        private CharacterMovementsSettings _movementsSettings;

        private IInputService _inputService;
        private ISpawnPointsStorage _spawnPointsStorage;

        public bool IsLocalPlayer => isLocalPlayer;
        public bool CanMove => _canMove;
        public float RotationX => _rotationX;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _spawnPointsStorage = AllServices.Container.Single<ISpawnPointsStorage>();
            _characterController = GetComponent<CharacterController>();
            _movementsSettings = AllServices.Container.Single<IAssetProvider>()
                .GetScriptableObject<GameSettings>(AssetPath.GameSettingsPath).CharacterMovementsSettings;
            SetToSpawnPoint();
        }

        private void SetToSpawnPoint()
        {
            _characterController.enabled = false;
            var spawnPoint = _spawnPointsStorage.GetSpawnPoint();
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
            _characterController.enabled = true;
        }

        private void Start()
        {
            LockCursor();
        }

        private void Update()
        {
            if (!isLocalPlayer) return;
            RecalculateBaseMoveDirection();
            CalculateMovement();
            CalculateJump();
            CalculateGravity();
            ApplyMovements();
            CalculateRotation();
        }

        private void CalculateMovement()
        {
            _isRunning = _inputService.IsRunning;
            _currentSpeedX = _canMove ? GetSpeed() * _inputService.VerticalSpeed : 0;
            _currentSpeedY = _canMove ? GetSpeed() * _inputService.HorizontalSpeed : 0;
            _movementDirectionY = _moveDirection.y;
            _moveDirection = (_currentForward * _currentSpeedX) + (_currentRight * _currentSpeedY);
        }

        private void CalculateRotation()
        {
            if (!_canMove) return;
            _rotationX += -_inputService.MouseY * _movementsSettings.LookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -_movementsSettings.LookXLimit, _movementsSettings.LookXLimit);
            transform.rotation *= Quaternion.Euler(0, _inputService.MouseX * _movementsSettings.LookSpeed, 0);
        }

        private float GetSpeed()
        {
            return (_isRunning ? _movementsSettings.RunningSpeed : _movementsSettings.WalkingSpeed);
        }

        private void CalculateJump()
        {
            if (_inputService.JumpButton && _canMove && _characterController.isGrounded)
            {
                _moveDirection.y = _movementsSettings.JumpSpeed;
            }
            else
            {
                _moveDirection.y = _movementDirectionY;
            }
        }

        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void RecalculateBaseMoveDirection()
        {
            _currentForward = transform.TransformDirection(Vector3.forward);
            _currentRight = transform.TransformDirection(Vector3.right);
        }

        private void ApplyMovements()
        {
            _characterController.Move(_moveDirection * Time.deltaTime);
        }

        private void CalculateGravity()
        {
            if (_characterController.isGrounded) return;
            _moveDirection.y -= _movementsSettings.Gravity * Time.deltaTime;
        }
    }
}