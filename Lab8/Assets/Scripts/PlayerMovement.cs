using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float mSpeed;

    [SerializeField]
    float mJumpForce;

    [SerializeField]
    float mMouseSensitivity;

    Rigidbody mRigidbody;
    bool canJump;

    void Start ()
    {
        mRigidbody = GetComponent<Rigidbody>();
    }

    void Update ()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        float rotationX = Input.GetAxis ("Mouse X");

        if(!Mathf.Approximately(vertical, 0.0f) || !Mathf.Approximately(horizontal, 0.0f))
        {
            Vector3 direction = new Vector3(horizontal, 0.0f, vertical);
            direction = Vector3.ClampMagnitude(direction, 1.0f);
            transform.Translate (direction * mSpeed * Time.deltaTime);
        }

        if(!Mathf.Approximately(rotationX, 0.0f))
        {
            Quaternion quaternionX = Quaternion.AngleAxis (rotationX * mMouseSensitivity, Vector3.up);
            transform.localRotation = transform.localRotation * quaternionX;
        }

        if(canJump && Input.GetButtonDown("Jump"))
        {
            mRigidbody.AddForce(Vector3.up * mJumpForce, ForceMode.Impulse);
        }

        if(Input.GetButtonDown("Cancel"))
        {
            Application.LoadLevel (0);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Floor")
        {
            canJump = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.tag == "Floor")
        {
            canJump = false;
        }
    }
}
