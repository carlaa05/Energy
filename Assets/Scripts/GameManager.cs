using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public StartZone startZoneP1;
    public StartZone startZoneP2;
    public StartZone startZoneP1_image;
    public StartZone startZoneP2_image;

    public GameObject gameplayObjects;

    public TMP_Text titleText;
    public TMP_Text instructionText;
    public TMP_Text endText;
    public TMP_Text scoreText;

    public GameObject player1;
    public GameObject player2;

    private bool gameStarted;
    private bool roundEnding;

    private int player1Wins;
    private int player2Wins;

    void Start()
    {
        gameStarted = false;
        roundEnding = false;

        if (gameplayObjects != null)
            gameplayObjects.SetActive(false);

        endText.gameObject.SetActive(false);
        UpdateScoreText();
    }

    void Update()
    {
        if (!gameStarted && !roundEnding)
        {
            if (startZoneP1.activated && startZoneP2.activated)
            {
                StartGame();
            }
        }

        if (gameStarted && !roundEnding)
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
        ResetPlayer(player1);
        ResetPlayer(player2);

        player1.GetComponent<EnergyTrail>().StartDrawing();
        player2.GetComponent<EnergyTrail>().StartDrawing();

        titleText.gameObject.SetActive(false);
        instructionText.gameObject.SetActive(false);
        endText.gameObject.SetActive(false);

        startZoneP1.gameObject.SetActive(false);
        startZoneP2.gameObject.SetActive(false);
        startZoneP1_image.gameObject.SetActive(false);
        startZoneP2_image.gameObject.SetActive(false);

        if (gameplayObjects != null)
            gameplayObjects.SetActive(true);
    }

    void EndGame(string message)
    {
        gameStarted = false;
        roundEnding = true;

        if (message == "PLAYER 1 WINS")
            player1Wins++;

        if (message == "PLAYER 2 WINS")
            player2Wins++;

        UpdateScoreText();

        endText.gameObject.SetActive(true);
        endText.text = message;

        StartCoroutine(RestartRound());
    }

    IEnumerator RestartRound()
    {
        yield return new WaitForSeconds(3f);

        ClearRoundObjects();

        ResetPlayer(player1);
        ResetPlayer(player2);

        startZoneP1.activated = false;
        startZoneP2.activated = false;

        titleText.gameObject.SetActive(true);
        instructionText.gameObject.SetActive(true);
        endText.gameObject.SetActive(false);

        startZoneP1.gameObject.SetActive(true);
        startZoneP2.gameObject.SetActive(true);
        startZoneP1_image.gameObject.SetActive(true);
        startZoneP2_image.gameObject.SetActive(true);

        if (gameplayObjects != null)
            gameplayObjects.SetActive(false);

        roundEnding = false;
    }

    void ClearRoundObjects()
    {
        GameObject[] stars = GameObject.FindGameObjectsWithTag("Energy");

        foreach (GameObject star in stars)
        {
            Destroy(star);
        }

        AsteroidMover[] enemies = FindObjectsByType<AsteroidMover>(FindObjectsSortMode.None);

        foreach (AsteroidMover enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }

    void ResetPlayer(GameObject player)
    {
        player.SetActive(true);

        EnergyTrail trail = player.GetComponent<EnergyTrail>();

        if (trail != null)
        {
            trail.ResetTrail();
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "P1 " + player1Wins + " - " + player2Wins + " P2";
        }
    }

    public bool IsGameActive()
    {
        return gameStarted && !roundEnding;
    }
}
