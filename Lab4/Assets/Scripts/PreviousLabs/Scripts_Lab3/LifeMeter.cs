using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeMeter : MonoBehaviour
{
    [SerializeField]
    int mMaxHealth;
    int mIndex;

    SpriteRenderer[] mFilled;
//    SpriteRenderer[] mEmpty;

    MegaMan mMegaMan;

    void Start ()
    {
        mMegaMan = GameObject.Find("Mega Man").GetComponent<MegaMan>();
        mFilled = transform.Find("Filled").GetComponentsInChildren<SpriteRenderer>();
//        mEmpty = transform.FindChild("Empty").GetComponentsInChildren<SpriteRenderer>();
        mMaxHealth = mFilled.Length;
        mIndex = mFilled.Length - 1;
    }

    public void AddHealth(int x)
    {
        for(int i = 0; i < x; i++)
        {
            mFilled[mIndex].enabled = true;
            if(mIndex == mMaxHealth)
            {
                break;
            }
            mIndex++;
        }
    }

    public void DeductHealth(int x)
    {
        for(int i = 0; i < x; i++)
        {
            mFilled[mIndex].enabled = false;
            mIndex--;
            if(mIndex < 0)
            {
                // Kill Megaman
                mMegaMan.Die ();
                break;
            }
        }
    }
}
