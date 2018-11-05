using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByHit : MonoBehaviour
{

    public GameObject explosion;
    public int maxHit;

    public BoxCollider leftCollider;
    public BoxCollider rightCollider;
    //public SphereCollider shieldCollider;
    public SphereCollider coreCollider;

    private GameController gameController;
    private int hit;
    private int leftHit;
    private int rightHit;
    private float timer;

    // Use this for initialization
    void Start()
    {
        hit = 0;
        leftHit = 0;
        rightHit = 0;
        timer = 5.0f;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
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
        Debug.Log("Collision");
        if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "EnemyA" || other.tag == "EnemyB" || other.tag == "Shield")
        {
            return;
        }

        var colliderId = GetComponent<Collider>().GetInstanceID();
        
        if (leftHit >= 2 && rightHit >= 2)
        {
            // TODO hide shield
            if (colliderId == coreCollider.GetInstanceID())
            {
                // hit core
                Destroy(other.gameObject);
                if (++hit >= 15)
                {
                    Destroy(gameObject);
                    if (explosion != null)
                    {
                        Instantiate(explosion, transform.position, transform.rotation);
                    }
                }
                else
                {
                    // show effect
                    Debug.Log("Core was hit: " + hit);
                }
            }
            else
            {
                Debug.Log("Only core should be hit!");
            }
            
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                leftHit = 0;
                rightHit = 0;
                timer = 5.0f;
            }

        }
        else
        {
            if (colliderId == leftCollider.GetInstanceID())
            {
                Destroy(other.gameObject);
                leftHit++;
                Debug.Log("Left was hit: " + leftHit);
            }
            else if (colliderId == rightCollider.GetInstanceID())
            {
                Destroy(other.gameObject);
                rightHit++;
                Debug.Log("Right was hit: " + rightHit);
            }
        }

    }
}
