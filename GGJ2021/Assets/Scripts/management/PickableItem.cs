using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public GameObject PickUpUI;
    GameManager manager;

    private void Start()
    {
        manager = GameManager.m_instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerInteract")
        {
            InteractWithPlayer();
        }
    }

    void InteractWithPlayer()
    {
        manager.InteractWithItem(gameObject.name);
    }

    void HighlightInteractable()
    {
        manager.InteractWithItem(gameObject.name);
    }
}
