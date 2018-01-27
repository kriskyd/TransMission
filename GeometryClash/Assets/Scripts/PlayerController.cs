using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum Geometry { triangle, square, circle }
    public Geometry geometry;
    public float moveSpeed, rotateSpeed;
    Vector3 move, rotation;
    public GameObject normalShotPrefab, superShotPrefab, ultimateShotPrefab;
    public string name;
    public int playerID;
    private bool shot = false;


    public int lifeTotal;
    public int energyTotal = 15;
    public Slider lifeSlider;
    public Slider energySlider;
    public float superCD = 0f, maxSuperCD = 5f;

    public void DoInit ()
    {

    }

    public void DoUpdate ()
    {
        Move ();
        Rotate ();
        Shoot ();
        superCD -= Time.deltaTime;
        if (superCD <= 0f)
            SuperShoot ();



        lifeSlider.value = lifeTotal;
        energySlider.value = energyTotal;
        checkKeyboardMove ();

    }

    private void Move ()
    {
        move.x = Input.GetAxisRaw (gameObject.name + " x-move");
        move.y = -Input.GetAxisRaw (gameObject.name + " y-move");
        move *= moveSpeed * Time.deltaTime;
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
        if (Input.GetAxisRaw (gameObject.name + " right-trigger") > 0.5f)
        {
            if (!shot)
            {
                NormalShot ns = Instantiate (normalShotPrefab, transform.position, transform.rotation).GetComponent<NormalShot> ();
                ns.DoInit (this);
                shot = true;
            }
        }
        else
            shot = false;
    }

    private void SuperShoot ()
    {
        if (Input.GetAxisRaw (gameObject.name + " left-trigger") > 0.5f)
        {
            superCD = maxSuperCD;
            switch (geometry)
            {
                case Geometry.circle:
                    SuperShot ss = Instantiate (superShotPrefab, transform.position, transform.rotation).GetComponent<SuperShot> ();
                    ss.DoInit (this);
                    break;
                case Geometry.square:
                    NormalShot ns = Instantiate (superShotPrefab, transform.position, transform.rotation).GetComponent<NormalShot> ();
                    ns.DoInit (this);
                    break;
            }
        }
    }

    private void UltimateShoot ()
    {
        if (Input.GetKeyDown ("joystick " + playerID.ToString () + " button 5"))
        {
            switch (geometry)
            {
                case Geometry.circle:

                    break;
                case Geometry.square:
				SquareUltimate ulti = Instantiate (ultimateShotPrefab, transform.position, transform.rotation).GetComponent<SquareUltimate> ();
				ulti.DoInit (this);
				break;
			}
			energyTotal -= 100;
        }
    }

    private void Shield ()
    {
        if (Input.GetKeyDown ("joystick " + playerID.ToString () + " button 4"))
        {

        }
    }


    void OnTriggerEnter2D (Collider2D other)
    {
        //print ("TAG " + other.gameObject.tag);
        if (other.CompareTag ("Trap"))
        {
            ReceiveDamage (other.GetComponent<TrapController> ().damage);
        }
        else if (other.CompareTag ("Pickup"))
        {
            ReceiveEnergy (other.GetComponent<Pickup> ().energy);
			Destroy (other.gameObject);

        }
        else if (other.CompareTag ("Bullet"))
        {
            if (other.GetComponent<NormalShot> ().parent != this)
            {
                ReceiveDamage (other.GetComponent<NormalShot> ().damage);
                Destroy (other.gameObject);
            }
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
        if (Input.GetKey (KeyCode.Space))
        {
            NormalShot ns = Instantiate (normalShotPrefab, transform.position, transform.rotation).GetComponent<NormalShot> ();
            ns.DoInit (this);
        }
        transform.Translate (move * moveSpeed * Time.deltaTime);
    }

    public void ReceiveDamage (int dmg)
    {
        lifeTotal -= dmg;
        IsDead ();
        print (lifeTotal);
    }

    public void ReceiveEnergy (int energy)
    {
        energyTotal += energy;
        energyTotal = energyTotal > 100 ? 100 : energyTotal;
    }

    public void UseEnergy (int energy)
    {
        energyTotal -= energy;
        energyTotal = energyTotal < 0 ? 0 : energyTotal;
    }

    public void Reset (Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        lifeTotal = 100;
        lifeSlider.value = lifeTotal;
        superCD = 0f;

        if (GameController.Current.gameState == GameController.GameState.End)
        {
            energyTotal = 15;
            energySlider.value = energyTotal;
        }

        foreach (NormalShot ns in FindObjectsOfType<NormalShot> ().ToList ())
        {
            Destroy (ns.gameObject);
        }
    }

    public bool IsDead ()
    {
        if (lifeTotal <= 0)
        {
            return true;
        }
        return false;
    }

}
