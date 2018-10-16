using UnityEngine;
using System.Collections;

public class Plunger : MonoBehaviour
{
    // Recommendation: Make the magnitude of the force adjustable from the inspector

    // TODO: Check if the ball is in the trigger zone.
    //       If it is and the player presses the launch button,
    //       then add a force to push the ball forward.
    //       Don't forget to use ForceMode.Impulse!

    // Can be done in 1 line of code in an if-statement in a function

    public float thrust;

    private GameObject ballGameObject;
    private Rigidbody ballRigidbody;

    private void Start()
    {
        ballGameObject = GameObject.FindGameObjectWithTag("Ball");
        ballRigidbody = ballGameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        var plungerPos = gameObject.transform.position;
        var ballPos = ballGameObject.transform.position;
        if (plungerPos.z - ballPos.z == 0 && Mathf.Abs(plungerPos.x - ballPos.x) < 0.2 && Input.GetButtonDown("Launch"))
        {
            ballRigidbody.AddForce(Vector3.forward * thrust, ForceMode.Impulse);
        }
    }
}