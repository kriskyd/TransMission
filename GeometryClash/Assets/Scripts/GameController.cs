using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{
    public static GameController Current;

    public enum GameState { Start, R1, B1, R2, B2, R3, B3, End }
    public GameState gameState, nextRound;

    public PlayerController playerOne, playerTwo;

    public List<TrapController> traps;

    private Vector3 p1StartPos, p2StartPos;
    public int roundCount = 0, maxRounds = 3, p1WinCount = 0, p2WinCount = 0, maxWins = 2;
    public float roundTime, maxRoundTime = 60f, breakTime, maxBreakTime = 3f;
    public bool playerDied = false;
    public Text timer, starter, p1WinText, p2WinText;


	public GameObject pickupPrefab;
	public float respawnTime = 0f, maxRespawnTime = 5f;

    void Start ()
    {
        Current = this;
    

        traps = FindObjectsOfType<TrapController> ().ToList ();
        for (int i = 0; i < traps.Count; i++)
        {
            traps [i].DoInit ();
        }


        playerOne.DoInit ();
        playerTwo.DoInit ();


		RespawnPickups ();


        p1StartPos = playerOne.transform.position;
        p2StartPos = playerTwo.transform.position;
        roundTime = maxRoundTime;
        breakTime = maxBreakTime;

		p1WinText.text = "0";
		p2WinText.text = "0";

    }

    void Update ()
    {
        switch (gameState)
        {
            case GameState.Start:
                timer.text = ((int) maxRoundTime).ToString ();
                starter.text = "Start Game\nPress A / M";
                if (Input.GetKeyDown (KeyCode.Joystick1Button0) || Input.GetKeyDown (KeyCode.M))
                {
                    RoundReset ();
                }
                break;
		case GameState.R1:
		case GameState.R2:
		case GameState.R3:
			roundTime -= Time.deltaTime;
			timer.text = ((int)roundTime + 1).ToString ();

			if (roundTime <= -1) {
				CheckEndTime ();
				break;
			}

			playerOne.DoUpdate ();
			playerTwo.DoUpdate ();

			CheckForDeadPlayer ();

			respawnTime -= Time.deltaTime;
			if (respawnTime <= 0f)
				RespawnPickups ();
                break;
		case GameState.B1:
		case GameState.B2:
		case GameState.B3:
			starter.gameObject.SetActive (true);
			breakTime -= Time.deltaTime;
			starter.text = "Round " + (roundCount + 1).ToString() + "\n" + ((int)breakTime + 1).ToString ();
			if (breakTime <= 0f) {
				starter.gameObject.SetActive (false);
				roundCount++;
				gameState = nextRound;
				breakTime = maxBreakTime;
			}

                break;
            case GameState.End:
			starter.gameObject.SetActive (true);

			string text = p1WinCount > p2WinCount ? "Kwadrat Wygrał" : "Koło wygrało";

			starter.text = text;
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
            p1WinText.text = p1WinCount.ToString ();
            p2WinText.text = p2WinCount.ToString ();

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

	private void CheckEndTime()
	{
		if (playerOne.lifeTotal >= playerTwo.lifeTotal)
		{
			p1WinCount++;
		}
		else
		{
			p2WinCount++;
		}


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

	private void RespawnPickups()
	{
		print("RespawnPickups " + respawnTime);
		foreach (Pickup item in FindObjectsOfType<Pickup> ().ToList ())
		{
			Destroy (item.gameObject);
		}


		Vector3 position = new Vector3 (-5.0f, 6.0f, -1.0f);
		Pickup pick = Instantiate (pickupPrefab, position, transform.rotation).GetComponent<Pickup> ();

		position = new Vector3 (5.0f, 6.0f, -1.0f);
		pick = Instantiate (pickupPrefab, position, transform.rotation).GetComponent<Pickup> ();

		position = new Vector3 (5.0f, -6.0f, -1.0f);
		pick = Instantiate (pickupPrefab, position, transform.rotation).GetComponent<Pickup> ();

		position = new Vector3 (-5.0f, -6.0f, -1.0f);
		pick = Instantiate (pickupPrefab, position, transform.rotation).GetComponent<Pickup> ();

		respawnTime = maxRespawnTime;

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
