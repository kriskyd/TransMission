using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed, rotateSpeed;
    Vector3 move, rotation;
    public GameObject normalShotPrefab;


	public int lifeTotal;
	public int energyTotal;

    public void DoInit ()
    {

    }

    public void DoUpdate ()
    {
        Move ();
        Rotate ();
        Shoot ();



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

    private void Shoot ()
    {
        if (Input.GetButtonDown(gameObject.name + " right-shot"))
        {
            NormalShot ns = Instantiate (normalShotPrefab, transform.position, transform.rotation).GetComponent<NormalShot> ();
            ns.DoInit (this);
        }
    }


	private void checkKeyboardMove()
	{
		if (Input.GetKey (KeyCode.W)) {
			move.x += Vector2.up.magnitude;
		}
		if (Input.GetKey (KeyCode.A)) {
			move.y += Vector2.left.magnitude;
		}
		if (Input.GetKey (KeyCode.S)) {
			move.x -= Vector2.down.magnitude;
		}
		if (Input.GetKey (KeyCode.D)) {
			move.y -= Vector2.right.magnitude;
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			NormalShot ns = Instantiate (normalShotPrefab, transform.position, transform.rotation).GetComponent<NormalShot> ();
			ns.DoInit (this);
		}

		transform.Translate (move);
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		print ("TAG " + other.gameObject.tag);
		if (other.CompareTag ("Trap"))
		{
			this.lifeTotal -= 10;
		}
		if (other.CompareTag ("Pickup"))
		{
			this.energyTotal += 10;
		}
		if (other.CompareTag ("Bullet"))
		{
			this.energyTotal -= 10;
		}

	}



}
