using UnityEngine;
using System.Collections;

public class Mario : MonoBehaviour
{
    // Variables set in the inspector
    [SerializeField]
    float mWalkSpeed;
    [SerializeField]
    float mRunSpeed;
    [SerializeField]
    float mJumpForce;

    [SerializeField]
    LayerMask mWhatIsGround;
    float kGroundCheckRadius = 0.1f;

    // Booleans used to coordinate with the animator's state machine
    bool mRunning;
    bool mMoving;
	bool mJumping;
    bool mGrounded;
    bool mFalling;

    // References to other components (can be from other game objects!)
    Animator mAnimator;
    Rigidbody2D mRigidBody2D;
    Transform mSpriteChild;
    Transform mGroundCheck;

    void Start ()
    {
        // Get references to other components and game objects
        mAnimator = GetComponent<Animator>();
        mRigidBody2D = GetComponent<Rigidbody2D>();
        mSpriteChild = transform.FindChild ("MarioSprite");
        mGroundCheck = transform.FindChild ("GroundCheck");
    }

    void Update ()
    {
        CheckGrounded ();
        MoveCharacter ();
        CheckFalling ();

        // Update animator's variables
        mAnimator.SetBool("isRunning", mRunning);
        mAnimator.SetBool("isMoving", mMoving);
		

        // TODO: Tell animator if game object is grounded or not (use the variable "mGrounded")
	 mAnimator.SetBool("isGrounded", mGrounded);
        // TODO: Tell animator if game object is falling or not (use the variable "mFalling")
   	 mAnimator.SetBool("isFalling", mFalling);
	}

    private void CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mGroundCheck.position, kGroundCheckRadius, mWhatIsGround);
        foreach(Collider2D col in colliders)
        {
            if(col.gameObject != gameObject)
            {
                mGrounded = true;
                return;
            }
        }
        mGrounded = false;
    }

    private void MoveCharacter()
    {
        // TODO: Check if the player wants Mario to run (see input manager)
        //       and set the value of "mRunning" accordingly
	

// Check if we are running or not
        if(Input.GetButtonDown ("Run"))
        {
            mRunning = true;
        }
        if(Input.GetButtonUp("Run"))
        {
            mRunning = false;
        }

// Determine movement speed
        float moveSpeed = mRunning ? mRunSpeed : mWalkSpeed;

        // Check for movement
        mMoving = false;
        if(Input.GetButton ("Left"))
        {
            // Translate the game object
            transform.Translate (-Vector2.right * moveSpeed * Time.deltaTime);
            FaceDirection(-Vector2.right);
            mMoving = true;
        }
        if(Input.GetButton ("Right"))
        {
            // Translate the game object
            transform.Translate (Vector2.right * moveSpeed * Time.deltaTime);
            FaceDirection(Vector2.right);
            mMoving = true;
        }

// Check if we can jump
        if(!mJumping && Input.GetButtonDown ("Jump"))
        {
            mRigidBody2D.AddForce(Vector2.up * mJumpForce);
        }


        // TODO: Make Mario move when the player presses Left or Right!
        //       Also, move Mario walk/run at the appropriate speed.
        //       Use the variables "mWalkSpeed" and "mRunSpeed"!
        //       Don't forget to flip Mario when necessary (use the FaceDirection() function)

		/*
		if (mMoving){
			if (move > 0){
				FaceDirection(Vector2.right);
				if (mRunning){
					transform.Translate(Vector2.right * mRunSpeed * Time.deltaTime, Space.World);
				} else {
					transform.Translate(Vector2.right * mWalkSpeed * Time.deltaTime, Space.World);
				}
			}
			else {
				FaceDirection(-Vector2.right);
				if (mRunning){
					transform.Translate(-Vector2.right * mRunSpeed * Time.deltaTime, Space.World);
				} else {
					transform.Translate(-Vector2.right * mWalkSpeed * Time.deltaTime, Space.World);
				}
			}
		}
        // TODO: If Mario is on the ground, allow him to jump!
        //       Make use of the "mGrounded" variable, whose value is being changed by the CheckGrounded() function
		if (Input.GetKeyDown(KeyCode.Space)){
			mJumping = true;
		} else {
			mJumping = false;
		}
		*/
    }

    private void CheckFalling()
    {
        mFalling = mRigidBody2D.velocity.y < 0.0f;
    }

    private void FaceDirection(Vector2 direction)
    {
        // Flip the sprite (NOTE: Vector3.forward is positive Z in 3D. The Sprite is on XY plane!)
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back); 
        mSpriteChild.rotation = rotation3D;
    }
}




/*Things to add:
isGrounded
isFalling

Mario Jump
Mario Fall

All 3 go to Jump
Jump goes to Idle or fall
Fall goes to idle
*/
