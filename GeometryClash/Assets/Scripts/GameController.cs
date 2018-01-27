using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public static GameController Current;

    public enum GameState { Start, R1, B1, R2, B2, R3, B3, End }
    public GameState gameState, nextRound;

    public PlayerController playerOne, playerTwo;

    public List<Pickup> pickups;
    public List<TrapController> traps;
    public List<NormalShot> normalShots;

    private Vector3 p1StartPos, p2StartPos;
    private int roundCount = 0, maxRounds = 3, p1WinCount = 0, p2WinCount = 0, maxWins = 2;
    public float roundTime, maxRoundTime = 60f, breakTime, maxBreakTime = 5f;
    public bool playerDied = false;


    void Start ()
    {
        Current = this;
        pickups = FindObjectsOfType<Pickup> ().ToList ();
        for (int i = 0; i < pickups.Count; i++)
        {
            pickups [i].DoInit ();
        }

        traps = FindObjectsOfType<TrapController> ().ToList ();
        for (int i = 0; i < traps.Count; i++)
        {
            traps [i].DoInit ();
        }

        normalShots = new List<NormalShot> ();

        playerOne.DoInit ();
        playerTwo.DoInit ();



        p1StartPos = playerOne.transform.position;
        p2StartPos = playerTwo.transform.position;
        roundTime = maxRoundTime;
        breakTime = maxBreakTime;

    }

    void Update ()
    {
        switch (gameState)
        {
            case GameState.Start:
                if (Input.GetKeyDown (KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.M))
                {
                    RoundReset ();
                }
                break;
            case GameState.R1:
            case GameState.R2:
            case GameState.R3:
                playerOne.DoUpdate ();
                playerTwo.DoUpdate ();

                CheckForDeadPlayer ();
                break;
            case GameState.B1:
            case GameState.B2:
            case GameState.B3:
                breakTime -= Time.deltaTime;
                if (breakTime <= 0f)
                {
                    roundCount++;
                    gameState = nextRound;
                    breakTime = maxBreakTime;
                }
                break;
            case GameState.End:
                if (Input.GetKeyDown (KeyCode.Joystick1Button0) || Input.GetKeyDown (KeyCode.M))
                {
                    GameReset ();
                }
                break;
        }
    }

    private void CheckForDeadPlayer ()
    {
        if (playerOne.IsDead ())
        {
            p2WinCount++;
            playerDied = true;
        }
        else if (playerTwo.IsDead ())
        {
            p1WinCount++;
            playerDied = true;
        }

        if (playerDied)
        {
            if (p1WinCount == 2)
            {
                EndGame (playerOne);
            }
            else if (p2WinCount == 2)
            {
                EndGame (playerTwo);
            }
            else
            {
                RoundReset ();
            }
            playerDied = false;
        }

    }

    public void RoundReset ()
    {
        switch (roundCount)
        {
            case 0:
                gameState = GameState.B1;
                nextRound = GameState.R1;
                break;
            case 1:
                gameState = GameState.B2;
                nextRound = GameState.R2;
                break;
            case 2:
                gameState = GameState.B3;
                nextRound = GameState.R3;
                break;
        }
        playerOne.Reset (p1StartPos);
        playerTwo.Reset (p2StartPos);

        roundTime = maxRoundTime;
    }

    void EndGame (PlayerController winner)
    {
        gameState = GameState.End;
    }

    void GameReset ()
    {
        playerOne.Reset (p1StartPos);
        playerTwo.Reset (p2StartPos);
        roundCount = 0;
        roundTime = maxRoundTime;
        p1WinCount = p2WinCount = 0;
        gameState = GameState.Start;
    }


}
