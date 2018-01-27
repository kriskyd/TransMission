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

    void Update ()
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
        move *= moveSpeed * Time.deltaTime;
        print (move);
        transform.Translate (move, Space.World);




    }

    private void Rotate ()
    {
        rotation.x = Input.GetAxisRaw (gameObject.name + " x-rotate");
        rotation.y = -Input.GetAxisRaw (gameObject.name + " y-rotate");
        if (Mathf.Abs (rotation.x) > 0.2 || Mathf.Abs (rotation.y) > 0.2)
        {
            float angle = Vector2.SignedAngle (Vector2.right, rotation);
            transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, angle);
        }
    }

    private void Shoot ()
    {
        if (Input.GetButtonDown (gameObject.name + " right-shot"))
        {
            NormalShot ns = Instantiate (normalShotPrefab, transform.position, transform.rotation).GetComponent<NormalShot> ();
            ns.DoInit (this);
        }
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
	private void checkKeyboardMove ()
    {
        move = Vector3.zero;
        if (Input.GetKey (KeyCode.W))
        {
            move.y += Vector2.up.magnitude;
        }
        if (Input.GetKey (KeyCode.A))
        {
            move.x -= Vector2.left.magnitude;
        }
        if (Input.GetKey (KeyCode.S))
        {
            move.y -= Vector2.down.magnitude;
        }
        if (Input.GetKey (KeyCode.D))
        {
            move.x += Vector2.right.magnitude;
        }
        transform.Translate (move);
    }
		



}
