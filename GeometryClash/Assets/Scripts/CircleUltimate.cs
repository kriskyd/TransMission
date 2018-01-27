using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleUltimate : Bullet
{

    public enum State { moving, hit, sizing }
    public State state;

    void Update ()
    {
        switch (state)
        {
            case State.moving:
                transform.Translate (transform.right * speed * Time.deltaTime, Space.World);
                break;
            case State.hit:
                transform.position = enemy.transform.position;
                if (transform.localScale.magnitude > enemy.transform.localScale.magnitude * 1.2f)
                    transform.localScale *= 0.95f;
                else
                    state = State.sizing;
                break;
            case State.sizing:

                break;
        }
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Arena")
            Destroy (gameObject);
    }

}
