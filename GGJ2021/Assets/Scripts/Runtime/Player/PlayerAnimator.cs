using Game.Runtime.Input;
using Game.Runtime.Utility;
using UnityEngine;

namespace Game.Runtime.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] protected MovementHandler MovementHandler;
        [SerializeField] protected Animator Animator;

        private Player PlayerObj;

        void Awake()
        {
            PlayerObj = transform.root.GetComponent<Player>();
        }
        
        private void Update()
        {
            float Velocity = MovementHandler.MovementVector.sqrMagnitude > 0.0f ? 1.0f : 0.0f;
            Animator.SetFloat(StringConstants.AnimationVelocity, Velocity);
        }

        public void ToggleMovement(bool State) 
            => PlayerObj.SetInputMode(State ? InputMode.Game : InputMode.Disable);
    }
}