using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCore : MonoBehaviour {

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
        Debug.Log(bossController.condition);
        if (bossController.condition)
        {
            Destroy(other.gameObject);
            hit++;
            bossController.UpdateHealth();
            Debug.Log("Core was hit: " + hit);
            if (hit >= maxHit)
            {
                bossController.Dead();
                gameController.Win();
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
