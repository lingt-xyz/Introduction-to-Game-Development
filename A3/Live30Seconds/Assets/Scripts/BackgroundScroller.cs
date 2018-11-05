using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    public float scrollSpeed;
    public float tileSizeZ;

    private Vector3 startPosition;
    private PlayerController playerController;
    private GameController gameController;

    private bool stop;
    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        stop = false;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script.");
        }

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition - Vector3.forward * newPosition;

        /*
        if (playerController.started)
        {
            if (!stop)
            {
                float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
                transform.position = startPosition + Vector3.forward * newPosition;
            }
            
        }
        if (gameController.level == 3)
        {
            stop = true;
        }
        */
    }

}
