using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    float smoothness;
    [SerializeField]
    Transform focusTarget;
    Vector3 relativePosition;
    Vector3 desiredPosition;

    [SerializeField]
    int maxInterpolations;

    void Awake()
    {
        relativePosition = transform.position - focusTarget.position;
    }

    void FixedUpdate()
    {
        // Check what camera position is best
        List<Vector3> camPositions = CalculatePositions();
        foreach(Vector3 p in camPositions)
        {
            if(CheckLineOfSight (p))
            {
                desiredPosition = p;
                break;
            }
        }

        // TODO: Smoothly translate the camera over to desired position

        //       One way of doing it:
        //       - Use Vector3.Lerp to go from "transform.position" to "desiredPosition".
        //       - Use the "smoothness" variable and Time.deltaTime to make it smooth

        // Can be done in 1 line of code:
        // transform.position = Vector3.Lerp(...)



        // TODO: Smoothly rotate camera to look at target

        //       One way of doing it:
        //       - Use Quaternion.LookRotation to obtain the desired rotation Quaternion (name it "desiredRotation")
        //       - Use Quaternion.Lerp to smoothly go from "transform.rotation" to the desired rotation "desiredRotation"
        //       - Use the "smoothness" variable with Time.deltaTime to make it smooth

        // Can be done in 2-3 lines of code
        // ... = Quaternion.LookRotation(...)
        // transform.rotation = Quaternion.Lerp(...)
    }

    List<Vector3> CalculatePositions()
    {
        Vector3 abovePosition = focusTarget.position + Vector3.up * relativePosition.magnitude;
        Vector3 standardPosition = focusTarget.position + relativePosition;
        
        List<Vector3> camPositions = new List<Vector3>();
        camPositions.Add (standardPosition);
        for(int i = 0; i < maxInterpolations; i++)
        {
            float t = (float)i / maxInterpolations;
            camPositions.Add (Vector3.Lerp (standardPosition, abovePosition, t));
        }
        camPositions.Add (abovePosition);

        return camPositions;
    }

    bool CheckLineOfSight(Vector3 position)
    {
        RaycastHit hit;
        if(Physics.Raycast(position, focusTarget.position - position, out hit, relativePosition.magnitude))
        {
            if(hit.transform != focusTarget)
            {
                return false;
            }
        }
        return true;
    }

    public void FocusTarget(Transform newTarget)
    {
        focusTarget = newTarget;
    }

    public Transform GetTarget()
    {
        return focusTarget;
    }
}
