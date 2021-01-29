using System.Collections;
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
        Coroutine LookAtLerpRoutine;
        Vector3 FinalDirection;

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
            TransformToMove = transform;
            if (LookAtLerpRoutine == null)
                LookAtLerpRoutine = StartCoroutine(SmoothRotate());
            FinalDirection = Vector3.left;
        }

        void Update()
        {
            if (_MovementVector.sqrMagnitude <= 0.0f) 
                return;
                
            Transform T = transform;
            Quaternion OffsetRotation = Quaternion.AngleAxis(45.0f, Vector3.up);
            Vector3 Vertical = OffsetRotation * T.forward * (_MovementVector.z * MovementSpeed * Time.deltaTime);
            Vector3 Horizontal = OffsetRotation * T.right * (_MovementVector.x * MovementSpeed * Time.deltaTime);
            FinalDirection = (Vertical + Horizontal).normalized;
            
            TransformToMove.Translate(Vertical + Horizontal, Space.World);

        }

        IEnumerator SmoothRotate()
        {
            while (true)
            {
                Quaternion ToRotate = Quaternion.LookRotation(FinalDirection, MeshToRotate.up);
                MeshToRotate.rotation = Quaternion.Lerp(MeshToRotate.rotation, ToRotate, 10 * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
    }
}