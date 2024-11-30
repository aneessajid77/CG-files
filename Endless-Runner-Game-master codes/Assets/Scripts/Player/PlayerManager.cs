using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;
    public GameObject newRecordPanel;

    public static int score;
    public Text scoreText;
    public TextMeshProUGUI gemsText;
    public TextMeshProUGUI newRecordText;

    public static bool isGamePaused;
    public GameObject[] characterPrefabs;

    private void Awake()
    {
        int index = PlayerPrefs.GetInt("SelectedCharacter");
        GameObject go = Instantiate(characterPrefabs[index], transform.position, Quaternion.identity);
    }

    void Start()
    {
        score = 0;
        Time.timeScale = 1;
        gameOver = isGameStarted = isGamePaused = false;
    }

    void Update()
    {
        // Update UI
        gemsText.text = PlayerPrefs.GetInt("TotalGems", 0).ToString();
        scoreText.text = score.ToString();

        // Game Over
        if (gameOver)
        {
            Time.timeScale = 0;
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                newRecordPanel.SetActive(true);
                newRecordText.text = "New \nRecord\n" + score;
                PlayerPrefs.SetInt("HighScore", score);
            }
            else
            {
                // This section previously had ad-related code, which is now removed.
                // You can add additional logic here if needed, for example, to trigger events or bonuses.
            }

            gameOverPanel.SetActive(true);
            Destroy(gameObject);
        }

        // Start Game
        if (SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }
}
