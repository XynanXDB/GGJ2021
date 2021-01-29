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
            if(manager == null)
                manager = GameManager.m_instance;
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

    public void InteractWithPlayer() // use this to see if Item Should be attached to player hand
    {
        manager.InteractWithItem(gameObject.name);
    }

    void PopUpUI()
    {
        manager.PopUpUI(gameObject.name);
    }

    void PopDownUI()
    {
        manager.PopDownUI(gameObject.name);
    }
}
