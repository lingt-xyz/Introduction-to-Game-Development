using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform mTarget;
    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;

    float mArriveThreshold = 0.05f;

    void Update ()
    {
        if(mTarget != null)
        {
            // TODO: Make the enemy follow the target "mTarget"
            //       only if the target is close enough (distance smaller than "mFollowRange")
            if ((mTarget.position - this.transform.position).magnitude < mFollowRange)
            {
                transform.Translate((mTarget.position-this.transform.position).normalized * mFollowSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }
}
