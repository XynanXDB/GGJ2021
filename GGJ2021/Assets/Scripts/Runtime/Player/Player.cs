using Game.Runtime.Dialogue;
using Game.Runtime.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Runtime.Player
{
    public class Player : MonoBehaviour, RLWControls.IGameActions, ITalkable
    {
        [SerializeField] protected MovementHandler MovementHandler;
        [SerializeField] protected InteractionHandler InteractionHandler;
        private InputHandler InputHandler;
        
        [SerializeField] protected FSpeakerInfo SpeakerInfo = new FSpeakerInfo()
        {
            Name = "JB",
            InternalIdentifier = "MC"
        };
        
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

        public void OnDrop(InputAction.CallbackContext context)
        {
            if (context.performed)
                InteractionHandler.Drop();
        }

        public FSpeakerInfo GetSpeakerInfo() => SpeakerInfo;
        
        public void SendNativeCommand(string[] Data)
        {
            if (Data[0] != SpeakerInfo.InternalIdentifier) 
                return;

            switch (Data[1])
            {
                case "InputModeUI":
                    InputHandler.SetInputMode(InputMode.UI);
                    break;
                
                case "InputModeGame":
                    InputHandler.SetInputMode(InputMode.Game);
                    break;
            }
        }
        public void EnableMovement()
        {
            MovementHandler.enabled = true;
        }
        public void DisableMovement()
        {
            MovementHandler.enabled = false;
        }
    }
}