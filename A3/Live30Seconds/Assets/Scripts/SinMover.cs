using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMover : MonoBehaviour
{
    private Transform target;
    private float time;

    // Use this for initialization
    void Start()
    {
        time = 0.0f;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var sinMove = Mathf.Sin(time + Mathf.PI) * 0.09f;
        transform.position += Vector3.right * Mathf.Sin(time + Mathf.PI)* 0.09f;
        if (target != null)
        {
            Vector3 targetDir = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(-targetDir);
        }
        time += Time.deltaTime;
    }
}
