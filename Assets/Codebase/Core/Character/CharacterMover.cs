using Mirror;
using UnityEngine;

namespace Codebase.Core.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMover : NetworkBehaviour, ICameraTarget
    {
        [SerializeField] private float _walkingSpeed = 7.5f;
        [SerializeField] private float _runningSpeed = 11.5f;
        [SerializeField] private float _jumpSpeed = 8.0f;
        [SerializeField] private float _gravity = 20.0f;
        [SerializeField] private float _lookSpeed = 2.0f;
        [SerializeField] private float _lookXLimit = 45.0f;

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

        public bool IsLocalPlayer => isLocalPlayer;
        public bool CanMove => _canMove;
        public float RotationX => _rotationX;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
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
            _isRunning = Input.GetKey(KeyCode.LeftShift);
            _currentSpeedX = _canMove ? GetSpeed() * Input.GetAxis("Vertical") : 0;
            _currentSpeedY = _canMove ? GetSpeed() * Input.GetAxis("Horizontal") : 0;
            _movementDirectionY = _moveDirection.y;
            _moveDirection = (_currentForward * _currentSpeedX) + (_currentRight * _currentSpeedY);
        }

        private void CalculateRotation()
        {
            if (!_canMove) return;
            _rotationX += -Input.GetAxis("Mouse Y") * _lookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -_lookXLimit, _lookXLimit);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeed, 0);
        }

        private float GetSpeed()
        {
            return (_isRunning ? _runningSpeed : _walkingSpeed);
        }

        private void CalculateJump()
        {
            if (Input.GetButton("Jump") && _canMove && _characterController.isGrounded)
            {
                _moveDirection.y = _jumpSpeed;
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
            _moveDirection.y -= _gravity * Time.deltaTime;
        }
    }
}