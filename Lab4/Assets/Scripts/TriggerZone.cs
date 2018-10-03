using UnityEngine;
using System.Collections;

public class TriggerZone : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Trigger Enter");
    }
    
    void OnTriggerStay(Collider col)
    {
        Debug.Log("Trigger Stay");
    }
    
    void OnTriggerExit(Collider col)
    {
        Debug.Log("Trigger Exit");
    }
}
