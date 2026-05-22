using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public StartZone startZoneP1;
    public StartZone startZoneP2;

    public GameObject gameplayObjects;
    public TMP_Text titleText;
    public TMP_Text instructionText;
    public TMP_Text endText;

    public GameObject player1;
    public GameObject player2;

    private bool gameStarted;

    void Start()
    {
        gameStarted = false;

        if (gameplayObjects != null)
            gameplayObjects.SetActive(false);

        endText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!gameStarted)
        {
            if (startZoneP1.activated && startZoneP2.activated)
            {
                StartGame();
            }
        }

        if (gameStarted)
        {
            if (!player1.activeSelf)
                EndGame("PLAYER 2 WINS");

            if (!player2.activeSelf)
                EndGame("PLAYER 1 WINS");
        }
    }

    void StartGame()
    {
        gameStarted = true;

        titleText.gameObject.SetActive(false);
        instructionText.gameObject.SetActive(false);
        startZoneP1.gameObject.SetActive(false);
        startZoneP2.gameObject.SetActive(false);

        if (gameplayObjects != null)
            gameplayObjects.SetActive(true);
    }

    void EndGame(string message)
    {
        gameStarted = false;

        endText.gameObject.SetActive(true);
        endText.text = message;
    }
}