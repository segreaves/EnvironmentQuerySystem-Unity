using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EnvironmentQuerySystem
{
    public class InputHandler : MonoBehaviour, Controls.IPlayerActions
    {
        private Controls controls;
        public Vector2 MovementValue { get; private set; }

        private void Start()
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);
            controls.Player.Enable();
            controls.Player.Move.performed += OnMove;
        }

        private void OnDestroy()
        {
            controls.Player.Move.performed -= OnMove;
            controls.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementValue = context.ReadValue<Vector2>();
        }
    }
}
