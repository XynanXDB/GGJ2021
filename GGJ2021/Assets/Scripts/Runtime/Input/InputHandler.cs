using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game.Runtime.Input
{
    public enum InputMode
    {
        Game,
        UI
    }
    
    public class InputHandler
    {
        private RLWControls Controls;

        public void SetInputMode(InputMode Mode)
        {
            switch (Mode)
            {
                case InputMode.Game:
                {
                    Controls.UI.Disable();
                    Controls.Game.Enable();
                    Debug.Log("Game Input Enabled");
                    break;
                }
                case InputMode.UI:
                {
                    Controls.Game.Disable();
                    Controls.UI.Enable();
                    Debug.Log("UI Input Enabled");
                    break;
                }
            }
        }

        public InputHandler()
        {
            Controls = new RLWControls();
            Controls.Game.Enable();
        }

        public void SetGameCallbacks(RLWControls.IGameActions InGameActions) 
            => Controls.Game.SetCallbacks(InGameActions);
    }
}
