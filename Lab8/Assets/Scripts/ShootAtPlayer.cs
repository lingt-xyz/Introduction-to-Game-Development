using UnityEngine;
using System.Collections;

public class ShootAtPlayer : MonoBehaviour
{
    [SerializeField]
    Projectile projectilePrefab;
    Transform playerTransform;
    [SerializeField]
    float mFireCooldown;
    float timer;
    bool cooldownActive;

    void Start ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update ()
    {
        RaycastHit hit;
        Vector3 toPlayer = playerTransform.position - transform.position;
        if(!cooldownActive && Physics.Raycast(transform.position, transform.forward, out hit, toPlayer.magnitude))
        {
            if(hit.collider.tag == "Player")
            {
                Fire ();
            }
        }

        if(cooldownActive)
        {
            timer -= Time.deltaTime;
            if(timer <= 0.0f)
            {
                cooldownActive = false;
            }
        }
    }

    void Fire()
    {
        cooldownActive = true;
        timer = mFireCooldown;
        Instantiate (projectilePrefab, transform.position, transform.rotation);
    }
}
