using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public Transform player;
    public Transform[] spawnPoints;

    private int currentRound = 0;
    private int collectedItems = 0;
    private float textTime = 0f;

    public int[] requiredItemsPerRound;

    public TextMeshProUGUI roundText;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI gameResultText;

    public GameObject endGamePanel;

    private void Update()
    {
        if (roundText.IsActive())
        {
            textTime+=Time.deltaTime;
            if (textTime >= 3f)
            {
                roundText.gameObject.SetActive(false);
            }
        }
    }

    public void CollectItem()
    {
        collectedItems++;

        countText.text = $"Count: {collectedItems}";

        CheckRoundCondition();
    }

    public void CheckRoundCondition()
    {
        if (collectedItems >= requiredItemsPerRound[currentRound])
        {
            NextRound();
        }
    }

    public void NextRound()
    {
        currentRound++;

        if (currentRound >= spawnPoints.Length)
        {
            WinGame();
            return;
        }

        textTime = 0f;
        roundText.gameObject.SetActive(true);
        roundText.text = $"Round {currentRound+1} ended.\n Round {currentRound+2} starts.";
        player.position = spawnPoints[currentRound].position;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        endGamePanel.SetActive(true);
        gameResultText.text = "Congratulations!\n You win the game!";
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        endGamePanel.SetActive(true);
        gameResultText.text = "You lose the game\n Try again?";
    }
}
