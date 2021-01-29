using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    GameManager manager;

    private void Start()
    {
        manager = GameManager.m_instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerInteract")
        {
            PopUpUI();
            //InteractWithPlayer();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "PlayerInteract")
        {
            PopDownUI();
        }
    }

    void InteractWithPlayer()
    {
        manager.InteractWithItem(gameObject.name);
    }

    void PopUpUI()
    {
        Debug.Log("POPIN");
        manager.PopUpUI(gameObject.name);
    }

    void PopDownUI()
    {
        manager.PopDownUI(gameObject.name);
    }
}
