using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed, rotateSpeed;
    Vector3 move, rotation;

	public int lifeTotal;

    public void DoInit ()
    {

    }

    public void DoUpdate ()
    {
        Move ();
        Rotate ();




		checkKeyboardMove ();

    }

    private void Move ()
    {
        move.x = Input.GetAxisRaw (gameObject.name + " x-move");
        move.y = -Input.GetAxisRaw (gameObject.name + " y-move");
        move *= moveSpeed;

		transform.Translate (move, Space.World);




    }

    private void Rotate ()
    {
        rotation.x = Input.GetAxisRaw (gameObject.name + " x-rotate");
        rotation.y = -Input.GetAxisRaw (gameObject.name + " y-rotate");
        float angle = Vector2.SignedAngle (Vector2.right, rotation);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
    }


	private void checkKeyboardMove()
	{
		if (Input.GetKey (KeyCode.W)) {
			move.y += Vector2.up.magnitude;
		}
		if (Input.GetKey (KeyCode.A)) {
			move.x -= Vector2.left.magnitude;
		}
		if (Input.GetKey (KeyCode.S)) {
			move.y -= Vector2.down.magnitude;
		}
		if (Input.GetKey (KeyCode.D)) {
			move.x += Vector2.right.magnitude;
		}
		move *= moveSpeed;

		GetComponent<Rigidbody2D> ().MovePosition (transform.position + move);
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		print ("TAG " + other.gameObject.tag);
		if (other.CompareTag ("Trap"))
		{
			this.lifeTotal -= 10;
		}

	}



}
