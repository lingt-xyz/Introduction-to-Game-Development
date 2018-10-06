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
        Debug.Log(transform.position.x);
        transform.position = new Vector3(Mathf.Sin(Time.time + Mathf.PI / 2) * 5, transform.position.y, transform.position.z);
        Debug.Log(transform.position.x);
        if (target != null)
        {
            Vector3 targetDir = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(-targetDir);
        }
    }
}
