using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform mTarget;
    [SerializeField]
    Vector3 mOffset;

    void Update ()
    {
        transform.position = mTarget.position + mOffset;
    }
}
