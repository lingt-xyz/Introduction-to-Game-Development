using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public ParticleSystem smokeEffect;

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

        //StartCoroutine(SpawnFire());
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
        StartCoroutine(SpawnFire());
        /*
        for (int i = 0; i < 2; i++)
        {
            GameObject g = Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, i*30, 0));
            //g.transform.Rotate(1.0f, i * 1, 0.0f);
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
        */

    }

    IEnumerator SpawnFire()
    {
            GameObject g = Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, 180, 0));
        for (int i = 0; i < 12; i++)
        {
            yield return new WaitForSeconds(delay);
            //g.transform.Rotate(1.0f, i * 1, 0.0f);
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }

    }
    internal void UpdateHealth()
    {
        healthBar.value--;
    }

    internal void Dead()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        if (smokeEffect != null)
        {
            smokeEffect.Play();
        }
    }
}
