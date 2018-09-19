using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.transform.SetParent(null);
        }
    }
}
