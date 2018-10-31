using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour
{
    public void Play()
    {
        Application.LoadLevel (1);
    }
}
