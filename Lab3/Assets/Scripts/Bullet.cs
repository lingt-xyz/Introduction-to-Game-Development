using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float mSpeed;
    Rigidbody2D mRigidBody2D;

    void Awake ()
    {
        // Must be done in Awake() because SetDirection() will be called early. Start() won't work.
        mRigidBody2D = GetComponent<Rigidbody2D>();

        // Set a default direction
        mRigidBody2D.velocity = Vector2.right * mSpeed;
    }

    public void SetDirection(Vector2 direction)
    {
        mRigidBody2D.velocity = direction * mSpeed;
    }
}
