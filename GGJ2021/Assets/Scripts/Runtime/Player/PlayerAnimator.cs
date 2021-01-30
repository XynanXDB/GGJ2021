using System;
using Game.Utility;
using UnityEngine;

namespace Game.Runtime.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] protected MovementHandler MovementHandler;
        [SerializeField] protected Animator Animator;
        
        private void Update()
        {
            float VelocityRatio = MovementHandler.GetVelocity.magnitude / MovementHandler.GetMaxMovementSpeed;
            Animator.SetFloat(StringConstants.AnimationVelocity, VelocityRatio);
        }
    }
}