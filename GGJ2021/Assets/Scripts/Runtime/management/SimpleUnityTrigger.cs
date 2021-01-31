using UnityEngine;
using UnityEngine.Events;

public class SimpleUnityTrigger : MonoBehaviour
{
    public UnityEvent EnterEvent;

    public UnityEvent ExitEvent;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            EnterEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            ExitEvent.Invoke();
    }
}
