using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.m_instance.CameEarly();
            GameManager.m_instance.SendInfoToUI();
            GameManager.m_instance.StoptimerExit = true;
        }
    }
}
