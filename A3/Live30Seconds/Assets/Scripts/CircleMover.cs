using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMover : MonoBehaviour
{

    public float speed;
    public int offset;
    private Rigidbody rb;
    private float time;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        var moveSin = Mathf.Sin(time + offset * Mathf.PI / 6);
        var moveCos = Mathf.Cos(time + offset * Mathf.PI / 6);
        rb.velocity = transform.forward * speed + transform.right * moveSin + transform.up * moveCos;
        time += Time.deltaTime;
    }
}
