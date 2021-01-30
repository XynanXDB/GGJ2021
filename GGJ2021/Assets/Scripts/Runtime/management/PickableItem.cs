using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    GameManager manager;
    Vector3 popUpOriSize;

    private void Start()
    {
        manager = GameManager.m_instance;
        popUpOriSize = transform.GetChild(0).transform.localScale;
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
        Debug.Log("Interact");
        manager.InteractWithItem(gameObject.name);
    }

    void PopUpUI()
    {
        transform.GetChild(0).transform.localScale = popUpOriSize;
        manager.PopUpUI(gameObject.name);
    }

    void PopDownUI()
    {
        transform.GetChild(0).transform.localScale = popUpOriSize;
        manager.PopDownUI(gameObject.name);
    }
}
