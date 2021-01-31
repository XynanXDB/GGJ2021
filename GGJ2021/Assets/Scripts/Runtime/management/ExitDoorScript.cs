using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorScript : MonoBehaviour
{
    bool Triggerd = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !Triggerd)
        {
            Triggerd = true;
            GameManager.m_instance.CameEarly();
            GameManager.m_instance.StoptimerExit = true;
        }
    }
}
