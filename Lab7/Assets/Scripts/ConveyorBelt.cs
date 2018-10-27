using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour
{
    public float BeltSpeed;
    public Vector3 Direction;

    void OnCollisionStay(Collision col)
    {
        Rigidbody body = col.gameObject.GetComponent<Rigidbody>();
        body.velocity = BeltSpeed * Direction;
    }
}
