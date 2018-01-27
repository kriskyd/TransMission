using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController playerOne, playerTwo;



    void Start ()
    {
        playerOne.DoInit ();
        playerTwo.DoInit ();
    }

    void Update ()
    {
        playerOne.DoUpdate ();
        playerTwo.DoUpdate ();
    }
}
