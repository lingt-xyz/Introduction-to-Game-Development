using UnityEngine;
using System.Collections;

public class Ramp : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision Enter");
    }

    void OnCollisionStay(Collision col)
    {
        Debug.Log("Collision Stay");
    }

    void OnCollisionExit(Collision col)
    {
        Debug.Log("Collision Exit");
    }
}
