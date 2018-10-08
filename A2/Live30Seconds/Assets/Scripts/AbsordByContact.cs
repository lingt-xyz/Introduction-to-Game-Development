using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsordByContact : MonoBehaviour {
    
    private PlayerController playerController;

    // Use this for initialization
    void Start()
    {
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
            Destroy(gameObject);
            playerController.Upgrade();
        }
    }
}
