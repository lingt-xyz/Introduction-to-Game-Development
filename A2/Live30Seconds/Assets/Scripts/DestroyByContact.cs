using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{

    public GameObject explosion;
    public GameObject playerExplosion;
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
        if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "EnemyA" || other.tag == "EnemyB" || other.tag == "Shield")
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (gameObject.tag == "Shield")
        {
            // show effect
            Destroy(other.gameObject);
            return;
        }
        if (other.tag == "Player")
        {
            playerController.Downgrade();
            if (playerController.weaponLevel == 0)
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
                Destroy(other.gameObject);
            }
        }
        else
        {
            Destroy(other.gameObject);
        }

        gameController.AddScore(scoreValue);

        Destroy(gameObject);

        if (gameObject.tag == "EnemyA")
        {
            gameController.destroyEnemyShipA++;
        }
        else if (gameObject.tag == "EnemyB")
        {
            gameController.destroyEnemyShipB++;
        }
    }
}
