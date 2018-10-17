using UnityEngine;
using System.Collections;

public class Flipper : MonoBehaviour
{
    [SerializeField]
    float mForce;

    [SerializeField]
    Vector3 mDirection = Vector3.forward;

    [SerializeField]
    string mButtonName;

    [SerializeField]
    Vector3 mOffset;

    private Rigidbody mRigidbody;
    private HingeJoint hinge;

    void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();
    }

    void FixedUpdate()
    {
        // TODO: Detect player's input (You can use mButtonName - It's convenient, but configure it first in the inspector!)
        //       Apply the force at the appropriate direction and at the correct position (make use of mOffset)

        // IMPORTANT: You must know the difference between Local Space and World Space!
        //            Ask yourself these questions:
        //                When the flipper is moving or rotating, what is its LOCAL forward direction at any time? Is this changing?
        //                What about its World Space forward direction? Is this always the same?

        // Can be done in 2 lines of code

        if (Input.GetButton(mButtonName))
        {
            var force = Input.GetButton(mButtonName) ? mForce : -mForce;
            mRigidbody.AddForceAtPosition(mDirection.normalized * mForce, transform.position + mOffset);
        }
        else
        {
            mRigidbody.AddForceAtPosition(-mDirection.normalized * mForce, transform.position + mOffset);
        }
    }
}
