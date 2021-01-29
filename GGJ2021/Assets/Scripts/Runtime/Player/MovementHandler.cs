using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Runtime.Player
{
    public class MovementHandler : MonoBehaviour
    {
        [Tooltip("Translation")]
        [SerializeField] protected float MovementSpeed = 5.0f;
        [SerializeField] protected Transform MeshToRotate;
        private Vector3 _MovementVector;
        private Transform TransformToMove;
        
        public float HorizontalSpeed
        {
            set { _MovementVector.x = value; }
        }

        public float VerticalSpeed
        {
            set { _MovementVector.z = value; }
        }
        
        void Awake() 
            => TransformToMove = transform;

        void Update()
        {
            if (_MovementVector.sqrMagnitude <= 0.0f) 
                return;
                
            Transform T = transform;
            Quaternion OffsetRotation = Quaternion.AngleAxis(45.0f, Vector3.up);
            Vector3 Vertical = OffsetRotation * T.forward * (_MovementVector.z * MovementSpeed * Time.deltaTime);
            Vector3 Horizontal = OffsetRotation * T.right * (_MovementVector.x * MovementSpeed * Time.deltaTime);
            Vector3 FinalDirection = (Vertical + Horizontal).normalized;
            
            TransformToMove.Translate(Vertical + Horizontal, Space.World);

            MeshToRotate.LookAt(FinalDirection + TransformToMove.position);
        }
    }
}