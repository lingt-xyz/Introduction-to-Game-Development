using UnityEngine;
using System.Collections;

public class ReturnToMenu : MonoBehaviour
{
    void Update ()
    {
        if(Input.GetButtonDown ("Cancel"))
        {
            Application.LoadLevel (0);
        }
    }
}
