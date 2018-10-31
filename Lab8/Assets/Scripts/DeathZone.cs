using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    Score scoreRef;

    void Start()
    {
        scoreRef = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Bullet")
        {
            Destroy (col.gameObject);
        }
        else if (col.tag == "Enemy")
        {
            Destroy (col.gameObject);
            scoreRef.IncrementScore(1);
        }
        else if (col.tag == "Player")
        {
            Application.LoadLevel (0);
        }
    }
}
