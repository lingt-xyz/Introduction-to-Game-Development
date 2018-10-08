using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public GameObject explosion;
    public int leftHit;
    public int rightHit;
    public bool condition;
    public int coreHit;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;

    private Slider healthBar;

    private AudioSource audioSource;

    private float timer;

    // Use this for initialization
    void Start()
    {
        leftHit = 0;
        rightHit = 0;
        coreHit = 0;
        timer = 0;
        condition = false;

        healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Slider>();

        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(leftHit + ":" + rightHit + ":" + timer + ":" + condition);
        if (leftHit >= 2 && rightHit >= 2)
        {
            timer += Time.deltaTime;
            if (timer <= 5.0f)
            {
                condition = true;
                // valid hit
            }
            else
            {
                leftHit = 0;
                rightHit = 0;
                timer = 0;
            }
        }
        else
        {
            condition = false;
        }
    }

    void Fire()
    {
        for (int i = 0; i < 12; i++)
        {
            GameObject g = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            g.transform.Rotate(0.0f, i * 30, 0.0f);
            audioSource.Play();
        }

    }

    internal void UpdateHealth()
    {
        healthBar.value--;
    }

    internal void Dead()
    {
        Destroy(gameObject);
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            // show effect
        }
    }
}
