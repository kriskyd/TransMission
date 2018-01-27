using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWallCollider : MonoBehaviour
{

    private void OnTriggerEnter2D (Collider2D collision)
    {
		print ("TAG " + collision.gameObject.tag);

        Destroy (collision.gameObject);
    }
		
}
