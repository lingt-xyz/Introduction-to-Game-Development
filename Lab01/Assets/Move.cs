using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	
	[SerializeField] float mSpeed;

	Vector3 rotate_y_positive = new Vector3 (0, 20, 0);
	Vector3 rotate_y_negative = new Vector3 (0, -20, 0);

	int legs = 1;
	void walk ()
	{
		int step = legs % 4;
		switch (step) {
		case 1:
			transform.Rotate (rotate_y_positive, Space.World);
			break;
		case 2:
			transform.Rotate (rotate_y_negative, Space.World);
			break;
		case 3:
			transform.Rotate (rotate_y_negative, Space.World);
			break;
		case 0:
			transform.Rotate (rotate_y_positive, Space.World);
			legs = 0;
			break;
		default:
			break;
		}

		legs++;
	}

	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (Vector3.forward, Space.World);
			walk ();
		}

		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (Vector3.back, Space.World);
			walk ();
		}

		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector3.left, Space.World);
			walk ();
		}

		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector3.right, Space.World);
			walk ();
		}

	}
}
