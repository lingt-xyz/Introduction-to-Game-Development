using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCore : MonoBehaviour {

    public GameObject explosion;
    public int maxHit;

    private GameController gameController;
    private BossController bossController;
    private int hit;

    // Use this for initialization
    void Start()
    {
        hit = 0;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        GameObject bossControllerObject = GameObject.FindWithTag("Boss");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (bossControllerObject != null)
        {
            bossController = bossControllerObject.GetComponent<BossController>();
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

        Debug.Log(bossController.condition);
        if (bossController.condition)
        {
            Destroy(other.gameObject);
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                // show effect
            }

            hit++;
            bossController.UpdateHealth();
            Debug.Log("Core was hit: " + hit);
            if (hit >= maxHit)
            {
                bossController.Dead();
            }
            else
            {
                // show effect
            }
        }
        else
        {

        }
    }
}
