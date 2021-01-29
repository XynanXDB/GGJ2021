using UnityEngine;

namespace Game.Runtime.Player
{
    public class MovementHandler : MonoBehaviour
    {
        [SerializeField] protected float MovementSpeed = 5.0f;
        [SerializeField] protected Transform RotationReferenceTransform;
        [SerializeField] private Rigidbody Rigidbody;
        
        private Vector3 _MovementVector;
        private Transform TransformToMove;
        private float IsometricOffsetAngle;
        
        public float HorizontalSpeed
        {
            set { _MovementVector.x = value; }
        }

        public float VerticalSpeed
        {
            set { _MovementVector.z = value; }
        }
        
        void Awake()
        {
            if (RotationReferenceTransform == null)
                Debug.LogError("Rotation reference not set, movement won't work properly.");
            
            TransformToMove = transform;
        }

        void Update()
        {
            Transform T = transform;
            Quaternion OffsetRotation = Quaternion.AngleAxis(45.0f, Vector3.up);
            Vector3 Vertical = OffsetRotation * T.forward * (_MovementVector.z * MovementSpeed * Time.deltaTime);
            Vector3 Horizontal = OffsetRotation * T.right * (_MovementVector.x * MovementSpeed * Time.deltaTime);
            
            TransformToMove.Translate(Vertical + Horizontal, Space.World);
        }
    }
}