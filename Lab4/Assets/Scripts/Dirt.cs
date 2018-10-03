using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dirt : MonoBehaviour {

	public Text dirtCounterText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.childCount != 0) {
			dirtCounterText.text = "Counter: " + transform.childCount;
		} else {
			dirtCounterText.text = "You did it!";
		}
	}
}
