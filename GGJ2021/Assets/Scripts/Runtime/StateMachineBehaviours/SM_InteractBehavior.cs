using Game.Runtime.Player;
using Game.Runtime.Utility;
using UnityEngine;

namespace Game.Runtime.StateMachineBehaviours
{
    public class SM_InteractBehavior : StateMachineBehaviour
    {
        private PlayerAnimator Animate;
        
        public override void OnStateEnter(Animator Animator, AnimatorStateInfo StateInfo, int LayerIndex)
        {
            Animate = Animator.GetComponent<PlayerAnimator>();
            Animate.ToggleMovement(false);
        }

        public override void OnStateUpdate(Animator Animator, AnimatorStateInfo StateInfo, int LayerIndex)
        {
            base.OnStateUpdate(Animator, StateInfo, LayerIndex);

            if (StateInfo.normalizedTime < 1.0f) return;
            
            Animate.ToggleMovement(true);
            Animator.SetBool(StringConstants.PlayerAnimate_Interact, false);
        }
    }
}