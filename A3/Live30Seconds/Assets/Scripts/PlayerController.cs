using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public GameObject shotSpawn;
    public float fireRate;
    public GameObject playerExplosion;
    private float nextFire;
    private AudioSource audioSource;
    private Rigidbody rb;

    public int weaponLevel = 1;
    private GameObject shotSpawn1;
    private GameObject shotSpawn2;

    private GameController gameController;

    public bool started;


    // Use this for initialization
    void Start()
    {
        started = false;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script.");
        }

        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        started = true;
        if (started)
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                StartCoroutine(Fire(shotSpawn));
                if (weaponLevel >= 3)
                {
                    StartCoroutine(Fire(shotSpawn1));
                    StartCoroutine(Fire(shotSpawn2));
                }
            }
        }
        else
        {
            transform.Translate(5 * Vector3.up * Time.deltaTime);
            if(transform.position.y >= 0.0f)
            {
                started = true;
            }
        }
        
    }

    IEnumerator Fire(GameObject spawn)
    {
        Instantiate(shot, spawn.transform.position, spawn.transform.rotation);
        if (weaponLevel >= 2)
        {
            yield return new WaitForSeconds(0.05f);
            Instantiate(shot, spawn.transform.position, spawn.transform.rotation);
        }
        audioSource.Play();
    }


    internal void Upgrade()
    {
        weaponLevel++;
        if (weaponLevel == 3)
        {
            shotSpawn1 = Instantiate(shotSpawn, shotSpawn.transform.position + new Vector3(0.1f, 0.0f, 0.0f), Quaternion.Euler(new Vector3(0, 25, 0)));
            shotSpawn1.transform.parent = gameObject.transform;
            shotSpawn2 = Instantiate(shotSpawn, shotSpawn.transform.position + new Vector3(-0.1f, 0.0f, 0.0f), Quaternion.Euler(new Vector3(0, -25, 0)));
            shotSpawn2.transform.parent = gameObject.transform;
        }
    }

    internal void Downgrade()
    {

        if (weaponLevel == 3)
        {
            Destroy(shotSpawn1);
            Destroy(shotSpawn2);
        }
        weaponLevel--;
    }

    void FixedUpdate()
    {
        if (started)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            var movemont = new Vector3(moveHorizontal, moveVertical, 0.0f);
            rb.velocity = movemont * speed;

            rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
                rb.position.z
            );

            rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bolt"))
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().shakeDuration = 0.25f;
            Downgrade();
            if (weaponLevel == 0)
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
    }
}
