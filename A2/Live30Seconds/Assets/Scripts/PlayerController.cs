using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public GameObject shotSpawn;
    public float fireRate;

    private float nextFire;
    private AudioSource audioSource;
    private Rigidbody rb;

    public int weaponLevel = 1;
    private GameObject shotSpawn1;
    private GameObject shotSpawn2;

    private GameController gameController;


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

        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
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

    IEnumerator Fire(GameObject spawn)
    {
        Debug.Log("Weapon Levle: " + weaponLevel);
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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        var movemont = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movemont * speed;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
