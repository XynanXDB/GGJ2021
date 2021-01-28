using Game.Runtime.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Runtime.Player
{
    public class Player : MonoBehaviour, RLWControls.IGameActions
    {
        [SerializeField] protected InputHandler InputHandler;

        void Awake()
        {
            InputHandler.SetGameCallbacks(this);
        }

        public void OnUpDown(InputAction.CallbackContext context)
        {
            Debug.Log(context.ReadValue<float>());
        }

        public void OnLeftRight(InputAction.CallbackContext context)
        {
            Debug.Log(context.ReadValue<float>());
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                Debug.Log("Interact");
        }
    }
}