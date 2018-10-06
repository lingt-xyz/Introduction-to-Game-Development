using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMover : MonoBehaviour
{

    private Transform target;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * -Mathf.Sin(Time.time + Mathf.PI)* 0.05f * 1f;
        transform.Translate(transform.position += transform.right * Mathf.Sin(Time.time + Mathf.PI) * 0.01f * 1f, Space.World);
       // transform.position = new Vector3(transform.position.x + Mathf.Sin(Time.time + Mathf.PI) * 0.1f, transform.position.y, transform.position.z);
        if (target != null)
        {
            Vector3 targetDir = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(-targetDir);
        }
    }
}
