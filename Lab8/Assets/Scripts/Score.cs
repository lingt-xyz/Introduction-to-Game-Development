using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    [SerializeField]
    int score = 0;

    void Awake ()
    {
        GameObject otherScore = GameObject.FindGameObjectWithTag("Score");
        if(otherScore == null || otherScore == gameObject)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    
    public void IncrementScore(int amount)
    {
        score += amount;
    }

    public int GetScore()
    {
        return score;
    }
}
