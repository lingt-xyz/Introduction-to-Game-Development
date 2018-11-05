using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByFloor : MonoBehaviour {
    public GameObject explosion;
    private AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
        if (explosion != null)
        {
            GameObject explosionObject = Instantiate(explosion, other.transform.position, other.transform.rotation);
            Destroy(explosionObject, 1.0f);
        }
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
