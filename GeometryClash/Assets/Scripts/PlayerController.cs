﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed, rotateSpeed;
    Vector3 move, rotation;

    public void DoInit ()
    {

    }

    public void DoUpdate ()
    {
        Move ();
        Rotate ();
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


}
