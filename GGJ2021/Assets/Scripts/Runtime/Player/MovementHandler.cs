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
        [SerializeField] protected Transform MeshToAngle;
        private Vector3 _MovementVector;
        private Transform TransformToMove;
        Coroutine LookAtLerpRoutine;
        Coroutine TiltRoutine;
        Vector3 FinalDirection;
        float PrevRotationY;
        float CurrentRotationY;
        float TiltElapsedTime;
        bool TiltCW = false;
        bool TiltCCW = false;

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
            PrevRotationY = MeshToRotate.localEulerAngles.y;
            CurrentRotationY = MeshToRotate.localEulerAngles.y;
            MeshToAngle = MeshToRotate.GetChild(0);
        }

        void Update()
        {
            if (_MovementVector.sqrMagnitude <= 0.0f)
            {
                TiltCCW = false;
                TiltCW = false;
                return;
            }
                
            Transform T = transform;
            Quaternion OffsetRotation = Quaternion.AngleAxis(45.0f, Vector3.up);
            Vector3 Vertical = OffsetRotation * T.forward * (_MovementVector.z * MovementSpeed * Time.deltaTime);
            Vector3 Horizontal = OffsetRotation * T.right * (_MovementVector.x * MovementSpeed * Time.deltaTime);
            FinalDirection = (Vertical + Horizontal).normalized;

            CurrentRotationY = MeshToRotate.localEulerAngles.y;
            TransformToMove.Translate(Vertical + Horizontal, Space.World);
            // filter for Previous Value due to 180 limit on rotation
            if (CurrentRotationY < 0 && PrevRotationY > 0)
                PrevRotationY = -360 + PrevRotationY;
            else if (CurrentRotationY > 0 && PrevRotationY < 0)
                PrevRotationY = 360 + PrevRotationY;

            if (PrevRotationY < CurrentRotationY  && !TiltCW)
            {
                if (TiltRoutine != null)
                {
                    StopCoroutine(TiltRoutine);
                }
                TiltCW = true;
                TiltCCW = false;
                TiltRoutine = StartCoroutine(TiltMesh(true));
            }
            else if (PrevRotationY > CurrentRotationY && !TiltCCW)
            {
                if (TiltRoutine != null)
                {
                    StopCoroutine(TiltRoutine);
                }
                TiltCW = false;
                TiltCCW = true;
                TiltRoutine = StartCoroutine(TiltMesh(false));
            }
            PrevRotationY = CurrentRotationY;
        }

        IEnumerator SmoothRotate()
        {
            while (true)
            {
                Quaternion ToRotate = Quaternion.LookRotation(FinalDirection, MeshToRotate.up);
                MeshToRotate.rotation = Quaternion.Lerp(MeshToRotate.rotation, ToRotate, 10 * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator TiltMesh(bool CW)
        {
            TiltElapsedTime = 0;
            if (CW)
            {
                Debug.Log("CW");
                while (TiltElapsedTime < 1)
                {
                    TiltElapsedTime += Time.deltaTime;
                    MeshToAngle.localEulerAngles = Vector3.Lerp(Vector3.back * 30, Vector3.zero, TiltElapsedTime);
                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                Debug.Log("CCW");
                while (TiltElapsedTime < 1)
                {
                    TiltElapsedTime += Time.deltaTime;
                    MeshToAngle.localEulerAngles = Vector3.Lerp(Vector3.forward * 30, Vector3.zero, TiltElapsedTime);
                    yield return new WaitForEndOfFrame();
                }
            }

            TiltCW = false;
            TiltCCW = false;
            yield return null;
        }
    }
}