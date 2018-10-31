using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float mMinVelocity;
    [SerializeField]
    float mMaxVelocity;
    
    void Start ()
    {
        Rigidbody r = GetComponent<Rigidbody> ();
        r.velocity = transform.forward * Random.Range (mMinVelocity, mMaxVelocity);
    }
}
