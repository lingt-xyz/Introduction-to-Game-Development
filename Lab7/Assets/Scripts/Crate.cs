using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour
{
    [SerializeField]
    Color selectedColor;
    [SerializeField]
    Color unselectedColor;

    public void SetSelected(bool selected)
    {
        GetComponent<Renderer>().material.color = selected ? selectedColor : unselectedColor;
    }
}
