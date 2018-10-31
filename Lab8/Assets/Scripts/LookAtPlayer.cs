using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField]
    float mSmoothness;

    Transform playerTransform;

    void Start ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update ()
    {
        Quaternion targetQuaternion = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, Time.deltaTime * mSmoothness);
    }
}
