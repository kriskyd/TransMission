using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWallCollider : MonoBehaviour
{

    private void OnTriggerEnter2D (Collider2D collision)
    {
        //print ("TAG " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag ("Bullet"))
            Destroy (collision.gameObject);
    }

}
