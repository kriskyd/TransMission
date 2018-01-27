using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareUltimate : Bullet
{

	public void Update ()
	{
		transform.Translate (transform.right * speed * Time.deltaTime, Space.World);
	}

	private void OnCollisionEnter2D (Collision2D collision)
	{
		print ("TAG " + collision.gameObject.tag);

		if (collision.gameObject.tag == "Arena")
			Destroy (gameObject);
	}


	void OnBecameInvisible() {
		print ("OnBecameInvisible");

		// Destroy the bullet 
		Destroy(gameObject);
	} 

}
