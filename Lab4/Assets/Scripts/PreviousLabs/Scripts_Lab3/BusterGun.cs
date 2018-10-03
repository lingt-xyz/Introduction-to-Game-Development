using UnityEngine;
using System.Collections;

public class BusterGun : MonoBehaviour
{
    Animator mAnimator;
    bool mShooting;

    float kShootDuration = 0.25f;
    float mTime;

    [SerializeField]
    GameObject mBulletPrefab;
    MegaMan mMegaManRef;

    AudioSource mBusterSound;

    void Start ()
    {
        mAnimator = transform.parent.GetComponent<Animator>();
        mMegaManRef = transform.parent.GetComponent<MegaMan>();
        mBusterSound = GetComponent<AudioSource>();
    }

    void Update ()
    {
        if(Input.GetButtonDown ("Fire"))
        {
            // Shoot bullet
            GameObject bulletObject = Instantiate (mBulletPrefab, transform.position, Quaternion.identity) as GameObject;
            Bullet bullet = bulletObject.GetComponent<Bullet>();

            // Set direction of bullet
            bullet.SetDirection(mMegaManRef.GetFacingDirection());

            // Set animation params
            mShooting = true;
            mTime = 0.0f;

            // Play sound
            mBusterSound.Play ();
        }

        if(mShooting)
        {
            mTime += Time.deltaTime;
            if(mTime > kShootDuration)
            {
                mShooting = false;
            }
        }

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        mAnimator.SetBool ("isShooting", mShooting);
    }
}
