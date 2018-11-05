using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByFloor : MonoBehaviour {
    public GameObject explosion;
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
        Destroy(other.gameObject);
        if (explosion != null)
        {
            GameObject explosionObject = Instantiate(explosion, other.transform.position, other.transform.rotation);
            Destroy(explosionObject, 1.0f);
        }
    }
}
