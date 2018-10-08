using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bolt")
        {
            Vector3 position = other.transform.position;
            if (position.x <= -6.1)
            {// left
                other.transform.position = new Vector3(6.1f, position.y, position.z);
//                other.transform.Translate(12f, 0, 0);
            }
            else if (position.x >= 6.1)
            {// right
                other.transform.position = new Vector3(-6.1f, position.y, position.z);
                //              other.transform.Translate(-12f, 0, 0);
            }
            else if (position.z >= 15)
            {// top
                other.transform.position = new Vector3(position.x, position.y, 5.0f);
                other.transform.Translate(0, 0, -20);
            }
            else
            {
                Destroy(other.gameObject);
            }

        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
