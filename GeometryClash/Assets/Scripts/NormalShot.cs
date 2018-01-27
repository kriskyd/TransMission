using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShot : MonoBehaviour
{
    PlayerController parent;

    public void DoInit (PlayerController parent)
    {
        this.parent = parent;
    }

    public void Update ()
    {
        transform.Translate (transform.right, Space.World);
    }


}
