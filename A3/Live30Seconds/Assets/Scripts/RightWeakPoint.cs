using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWeakPoint : MonoBehaviour {
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
        if (other.gameObject.CompareTag("Boundary"))
        {
            return;
        }
        if (explosion != null)
        {
            GameObject explosionObject = Instantiate(explosion, other.transform.position, other.transform.rotation);
            Destroy(explosionObject, 1.0f);
            // show effect
        }
        Destroy(other.gameObject);
        bossController.rightHit++;
        Debug.Log("Right was hit: " + bossController.rightHit);

    }
}
