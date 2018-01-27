using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperShot : Bullet
{
    public float moveTime = 1f, lifeTime = 10f;

    public void DoInit (PlayerController parent)
    {
        this.parent = parent;
    }

    void Update ()
    {
        moveTime -= Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (moveTime > 0f)
            transform.Translate (transform.right * speed * Time.deltaTime, Space.World);
        if (lifeTime < 0f)
            Destroy (gameObject);
    }
}
