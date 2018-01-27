using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShot : MonoBehaviour
{
    PlayerController parent;
    public float speed;

    public void DoInit (PlayerController parent)
    {
        this.parent = parent;
        GameController.Current.normalShots.Add (this);
    }

    public void Update ()
    {
        transform.Translate (transform.right * speed * Time.deltaTime, Space.World);
    }


}
