using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWeakPoint : MonoBehaviour {
    public GameObject explosion;

    private GameController gameController;
    private BossController bossController;
    private int hit;
    private float timer;

    // Use this for initialization
    void Start()
    {
        hit = 0;
        timer = 5.0f;
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

        Destroy(other.gameObject);
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            // show effect
        }
        bossController.leftHit++;
        Debug.Log("Left was hit: " + bossController.leftHit);

    }
}
