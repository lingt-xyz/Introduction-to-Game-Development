using UnityEngine;
using System.Collections;

public class Jamminger : MonoBehaviour
{
    [SerializeField]
    GameObject mExplosionPrefab;

    void OnTriggerEnter2D(Collider2D col)
    {
        // TODO: Check if I am being hit by a bullet
        //       If that's the case, do the following:
        //          - Destroy the bullet
        //          - Destroy myself
        //          - Instantiate an explosion (use the prefab "mExplosionPrefab")
        if (col.gameObject.name.Equals("Bullet"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
            var explosion = Instantiate(mExplosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
            explosion.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        // TODO: Check if it's Mega Man (ignore everything else)
        //       If it is Mega Man, make him take some damage (e.g. 3 points)
        //       Use the TakeDamage() function from the MegaMan class!
        if (col.gameObject.name.Equals("Mega Man"))
        {
            col.GetComponent<MegaMan>().TakeDamage(3);
        }
    }
}
