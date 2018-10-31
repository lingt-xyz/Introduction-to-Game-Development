using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    Projectile projectilePrefab;
    [SerializeField]
    float mFireCooldown;
    float timer;
    bool cooldownActive;

    void Update ()
    {
        if(!cooldownActive && Input.GetButton ("Fire1"))
        {
            cooldownActive = true;
            timer = mFireCooldown;
            Instantiate (projectilePrefab, transform.position, transform.rotation);
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
}
