using UnityEngine;
using System.Collections;

public class TimedExpiration : MonoBehaviour
{
    [SerializeField]
    float mLifespan;
    float timer = 0.0f;

    void Update ()
    {
        timer += Time.deltaTime;
        if(timer > mLifespan)
        {
            Destroy(gameObject);
        }
    }
}
