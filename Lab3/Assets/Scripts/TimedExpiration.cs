using UnityEngine;
using System.Collections;

public class TimedExpiration : MonoBehaviour
{
    [SerializeField]
    float mExpirationTime;
    float mTimer;

    void Update ()
    {
        mTimer += Time.deltaTime;
        if(mTimer >= mExpirationTime)
        {
            Destroy (gameObject);
        }
    }
}
