using UnityEngine;

namespace Game.Runtime.Input
{
    public enum InputMode
    {
        Game,
        UI
    }
    
    public class InputHandler : MonoBehaviour
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

        void Awake()
        {
            Controls = new RLWControls();
            Controls.Game.Enable();
        }

        public void SetGameCallbacks(RLWControls.IGameActions InGameActions) 
            => Controls.Game.SetCallbacks(InGameActions);
    }
}
