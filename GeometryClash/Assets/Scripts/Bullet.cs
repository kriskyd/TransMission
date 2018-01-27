using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerController parent, enemy;
    public float speed;
    public int damage;

    public void DoInit (PlayerController parent)
    {
        this.parent = parent;
        if (parent.playerID == 1)
            enemy = GameController.Current.playerTwo;
        else
            enemy = GameController.Current.playerOne;
    }
}
