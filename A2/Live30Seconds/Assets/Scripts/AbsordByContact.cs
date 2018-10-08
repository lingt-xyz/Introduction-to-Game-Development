using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsordByContact : MonoBehaviour {
    
    private PlayerController playerController;
    private AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            audioSource.Play();
            Destroy(gameObject);
            playerController.Upgrade();
        }
    }
}
