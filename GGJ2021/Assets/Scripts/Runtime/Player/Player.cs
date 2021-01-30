using Game.Runtime.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Runtime.Player
{
    public class Player : MonoBehaviour, RLWControls.IGameActions
    {
        private InputHandler InputHandler;
        [SerializeField] protected MovementHandler MovementHandler;
        [SerializeField] protected InteractionHandler InteractionHandler;
        
        void Awake()
        {
            InputHandler = new InputHandler();
            InputHandler.SetGameCallbacks(this);

            TryGetComponent<MovementHandler>(out MovementHandler);
        }

        public void OnUpDown(InputAction.CallbackContext context) 
            => MovementHandler.VerticalSpeed = context.ReadValue<float>();

        public void OnLeftRight(InputAction.CallbackContext context) 
            => MovementHandler.HorizontalSpeed = context.ReadValue<float>();

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                InteractionHandler.Interact();
        }

        public void DisableMovement()
        {
            MovementHandler.enabled = false;
        }

        public void EnableMovement()
        {
            MovementHandler.enabled = true;
        }
    }
}