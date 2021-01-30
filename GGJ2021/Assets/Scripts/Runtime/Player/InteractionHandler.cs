using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime.Player
{
    [RequireComponent(typeof(Collider))]
    public class InteractionHandler : MonoBehaviour
    {
        [SerializeField] protected float FlipForce = 10.0f;
        [SerializeField] protected float ExplosionRadius = 2.0f;
        [SerializeField] protected float UpwardModifier = 1.0f;

        private List<Collider> FlippableObjects;
        public List<Collider> InteractableObjects;

        private const string Flippable = "Flippable";
        private const string Interactable = "Interactable";
        
        void Awake()
        {
            FlippableObjects = new List<Collider>();
            InteractableObjects = new List<Collider>();
        }

        void OnTriggerEnter(Collider Other)
        {
            if (Other.CompareTag(Flippable))
                FlippableObjects.Add(Other);
            
            else if (Other.CompareTag(Interactable))
                InteractableObjects.Add(Other);
        }

        void OnTriggerExit(Collider Other)
        {
            if (Other.CompareTag(Flippable))
                FlippableObjects.Remove(Other);
            
            else if (Other.CompareTag(Interactable))
                InteractableObjects.Remove(Other);
        }

        public void Interact(PickableItem Item = null)
        {
            if (FlippableObjects.Count > 0)
            {
                foreach (Collider C in FlippableObjects)
                {
                    Rigidbody RB = C.GetComponentInParent<Rigidbody>();

                    if (RB == null)
                        continue;

                    if (RB.isKinematic)
                    {
                        RB.isKinematic = false;
                        RB.useGravity = true;
                        RB.constraints = RigidbodyConstraints.None;
                    }
                    
                    RB.AddExplosionForce(FlipForce, transform.parent.position, ExplosionRadius, UpwardModifier,
                        ForceMode.Impulse);
                }

                return;
            }
            if (Item != null)
                Item.InteractWithPlayer();
        }

        public void Drop()
        {
            Debug.Log("Drop");
        }
    }
}
