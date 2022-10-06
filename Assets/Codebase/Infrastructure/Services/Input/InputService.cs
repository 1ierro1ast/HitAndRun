using System;
using System.Collections;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        public float HorizontalSpeed => UnityEngine.Input.GetAxis("Horizontal");
        public float VerticalSpeed => UnityEngine.Input.GetAxis("Vertical");
        public float MouseX => UnityEngine.Input.GetAxis("Mouse X");
        public float MouseY => UnityEngine.Input.GetAxis("Mouse Y");
        public bool JumpButton => UnityEngine.Input.GetButton("Jump");
        public bool IsRunning => UnityEngine.Input.GetKey(KeyCode.LeftShift);

        public event Action FireButtonEvent;

        private const float UpdateTime = 0.0002f;
        public InputService(ICoroutineRunner coroutineRunner)
        {
            coroutineRunner.StartCoroutine(InputHandlerCoroutine());
        }

        private IEnumerator InputHandlerCoroutine()
        {
            while (true)
            {
                HandleFireButton();
                yield return new WaitForSeconds(UpdateTime);
            }
        }


        private void HandleFireButton()
        {
            if(UnityEngine.Input.GetMouseButtonDown(0)) FireButtonEvent?.Invoke();
        }
    }
}