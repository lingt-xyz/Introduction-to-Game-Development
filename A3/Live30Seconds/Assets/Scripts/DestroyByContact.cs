using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public ParticleSystem smokeEffect;
    public int scoreValue;

    private GameController gameController;
    private PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script.");
        }

        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bolt") && (gameObject.CompareTag("EnemyA") || gameObject.CompareTag("EnemyB")))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            //Destroy(gameObject);
            Destroy(other.gameObject);
            if (smokeEffect != null)
            {
                smokeEffect.Play();
            }

            gameController.AddScore(scoreValue);

            if (gameObject.tag == "EnemyA")
            {
                gameController.destroyEnemyA++;
            }
            else if (gameObject.tag == "EnemyB")
            {
                gameController.destroyEnemyB++;
            }

        }
        if (other.gameObject.CompareTag("Bolt") && gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (gameObject.CompareTag("Bolt") && other.gameObject.CompareTag("Bolt"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (gameObject.CompareTag("Player") && (other.gameObject.CompareTag("EnemyA") || other.gameObject.CompareTag("EnemyB")))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
