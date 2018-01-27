using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShot : MonoBehaviour
{
    public PlayerController parent;
    public float speed;
    public int damage;

    public void DoInit (PlayerController parent)
    {
        this.parent = parent;
        GameController.Current.normalShots.Add (this);
    }

    public void Update ()
    {
        transform.Translate (transform.right * speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Arena")
            Destroy (gameObject);
    }

}
