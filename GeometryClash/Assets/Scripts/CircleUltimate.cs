using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleUltimate : Bullet
{

    public enum State { moving, hit, sizing }
    public State state;
    public bool size;
    public float timeToSize = 3.6f;
    Sequence tween;

    void Update ()
    {
        switch (state)
        {
            case State.moving:
                transform.Translate (transform.right * speed * Time.deltaTime, Space.World);
                break;
            case State.hit:
                timeToSize -= Time.deltaTime;
                if (timeToSize <= 0f)
                    state = State.sizing;
                break;
            case State.sizing:
                enemy.ReceiveDamage (damage);
                break;
        }
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Arena")
            Destroy (gameObject);
    }

    public void UseUltimate()
    {
        tween = DOTween.Sequence ();
        tween.Append (transform.DOMoveZ (1, 0.2f))
            .Append (transform.DOMove (enemy.transform.position, 2f))
            .Append (transform.DOScale (1.3f, 1f)).Append (transform.DOScale (2f, 0.3f))
            .Append (transform.DOScale (0.6f, 0.2f));
    }


}
