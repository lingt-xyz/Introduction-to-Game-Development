using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MegaMan : MonoBehaviour
{
    [SerializeField]
    float mMoveSpeed;
    [SerializeField]
    float mJumpForce;
    [SerializeField]
    LayerMask mWhatIsGround;
    float kGroundCheckRadius = 0.1f;

    // Animator booleans
    bool mRunning;
    bool mGrounded;
    bool mRising;

    // Invincibility timer
    float kInvincibilityDuration = 1.0f;
    float mInvincibleTimer;
    bool mInvincible;

    // Damage effects
    float kDamagePushForce = 2.5f;

    // Wall kicking
    bool mAllowWallKick;
    Vector2 mFacingDirection;

    // References to other components and game objects
    Animator mAnimator;
    Rigidbody2D mRigidBody2D;
    List<GroundCheck> mGroundCheckList;

    // Reference to audio sources
    AudioSource mLandingSound;
    AudioSource mWallKickSound;
    AudioSource mTakeDamageSound;

    [SerializeField]
    GameObject mDeathParticleEmitter;
    [SerializeField]
    LifeMeter life;

    void Start ()
    {
        // Get references to other components and game objects
        mAnimator = GetComponent<Animator>();
        mRigidBody2D = GetComponent<Rigidbody2D>();

		mFacingDirection = Vector2.right;

        // Obtain ground check components and store in list
        mGroundCheckList = new List<GroundCheck>();
        GroundCheck[] groundChecksArray = transform.GetComponentsInChildren<GroundCheck>();
        foreach(GroundCheck g in groundChecksArray)
        {
            mGroundCheckList.Add (g);
        }

        // Get audio references
        AudioSource[] audioSources = GetComponents<AudioSource>();
        mLandingSound = audioSources[0];
        mWallKickSound = audioSources[1];
        mTakeDamageSound = audioSources[2];
    }

    void Update ()
    {
        mRunning = false;
        if(Input.GetButton ("Left"))
        {
			transform.Translate (-Vector2.right * mMoveSpeed * Time.deltaTime, Space.World);
            FaceDirection(-Vector2.right);
            mRunning = true;
        }
        if(Input.GetButton ("Right"))
        {
			transform.Translate (Vector2.right * mMoveSpeed * Time.deltaTime, Space.World);
            FaceDirection(Vector2.right);
            mRunning = true;
        }

        bool grounded = CheckGrounded ();
        if(!mGrounded && grounded)
        {
            mLandingSound.Play ();
        }
        mGrounded = grounded;

        if(mGrounded && Input.GetButtonDown ("Jump"))
        {
            mRigidBody2D.AddForce(Vector2.up * mJumpForce, ForceMode2D.Impulse);
        }
        else if (mAllowWallKick && Input.GetButtonDown ("Jump"))
        {
            mRigidBody2D.velocity = Vector2.zero;
            mRigidBody2D.AddForce(Vector2.up * mJumpForce, ForceMode2D.Impulse);
            mWallKickSound.Play ();
        }

        mRising = mRigidBody2D.velocity.y > 0.0f;
        UpdateAnimator();

        if(mInvincible)
        {
            mInvincibleTimer += Time.deltaTime;
            if(mInvincibleTimer >= kInvincibilityDuration)
            {
                mInvincible = false;
                mInvincibleTimer = 0.0f;
            }
        }
    }

    public void Die()
    {
        // TODO: Instantiate the particle effects "mDeathParticleEmitter"
        //       and destroy the "Mega Man" game object
    }

    public void TakeDamage(int dmg)
    {
        if(!mInvincible)
        {
            Vector2 forceDirection = new Vector2(-mFacingDirection.x, 1.0f) * kDamagePushForce;
            mRigidBody2D.velocity = Vector2.zero;
            mRigidBody2D.AddForce(forceDirection, ForceMode2D.Impulse);
            mInvincible = true;
            mTakeDamageSound.Play ();
            life.DeductHealth(dmg);
        }
    }

    public Vector2 GetFacingDirection()
    {
        return mFacingDirection;
    }

    private void FaceDirection(Vector2 direction)
    {
        mFacingDirection = direction;
        if(direction == Vector2.right)
        {
            Vector3 newScale = new Vector3(Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
        else
        {
            Vector3 newScale = new Vector3(-Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
    }

    private bool CheckGrounded()
    {
        foreach(GroundCheck g in mGroundCheckList)
        {
            if(g.CheckGrounded(kGroundCheckRadius, mWhatIsGround, gameObject))
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateAnimator()
    {
        mAnimator.SetBool ("isRunning", mRunning);
        mAnimator.SetBool ("isGrounded", mGrounded);
        mAnimator.SetBool ("isRising", mRising);
        mAnimator.SetBool ("isHurt", mInvincible);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            ContactPoint2D[] contactPoints = col.contacts;
            foreach(ContactPoint2D p in contactPoints)
            {
                float angleDifference = Vector2.Angle(p.normal, Vector2.right);
                if(angleDifference < 5.0f || angleDifference > 175.0f)
                {
                    mAllowWallKick = true;
                    return;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.collider.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            mAllowWallKick = false;
        }
    }
}
