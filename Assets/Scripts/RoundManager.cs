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
    public AudioSource audioSource;
    public AudioClip winSound;

    private Rigidbody rb;

    private void Start()
    {
        Time.timeScale = 1f;
        rb = player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (roundText.gameObject.activeSelf)
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
        collectedItems = 0;
        currentRound++;

        if (currentRound >= spawnPoints.Length)
        {
            WinGame();
            return;
        }

        textTime = 0f;
        roundText.gameObject.SetActive(true);
        roundText.text = $"Round {currentRound} ended.\n Round {currentRound+1} starts.";
        player.position = spawnPoints[currentRound].position;
        
        rb.linearVelocity = Vector3.zero;
    }

    public void WinGame()
    {
        EndGame("Congratulations!\n You win the game!");
        audioSource.PlayOneShot(winSound);
    }

    public void LoseGame()
    {
        EndGame("You lose the game\n Try again?");
    }

    private void EndGame(string endText)
    {
        Time.timeScale = 0f;
        endGamePanel.SetActive(true);
        gameResultText.text = endText;
    }
}
