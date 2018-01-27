using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public static GameController Current;
    public PlayerController playerOne, playerTwo;

    public List<Pickup> pickups;
    public List<TrapController> traps;
    public List<NormalShot> normalShots;



    void Start ()
    {
        pickups = FindObjectsOfType<Pickup> ().ToList ();
        for (int i=0; i < pickups.Count; i++)
        {
            pickups[i].DoInit ();
        }

        traps = FindObjectsOfType<TrapController> ().ToList ();
        for (int i = 0; i < traps.Count; i++)
        {
            traps[i].DoInit ();
        }

        playerOne.DoInit ();
        playerTwo.DoInit ();
    }

    void Update ()
    {

    }
}
