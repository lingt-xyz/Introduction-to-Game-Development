using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreUI : MonoBehaviour
{
    Text mText;
    Score scoreRef;

    void Start ()
    {
        mText = GetComponent<Text>();
        scoreRef = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
    }

    void Update ()
    {
        mText.text = scoreRef.GetScore().ToString ();
    }
}
