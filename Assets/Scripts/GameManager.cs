using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    [SerializeField]
    private Player player;
    [SerializeField]
    private Spawner spawner;

    public TextMeshProUGUI gameOverText;
    public Button retryButton;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private float score;

    public bool isRunning { get; private set; }


    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    public void Start()
    {

        //player = FindObjectOfType<Player>();
        //spawner = FindObjectOfType<Spawner>();

        NewGame();
    }

    public void NewGame()
    {
        isRunning = true;

        Spawner.Instance.ClearGame();

        gameSpeed = initialGameSpeed;
        enabled = true;
        //player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        score = 0;

        UpdateHightScore();
    }


    public void GameOver() {
        isRunning = false;
        gameSpeed = 0f;
        enabled = false;
        //player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        UpdateHightScore();
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");

        player.GiveReward(gameSpeed * Time.deltaTime*0.1f);
    }

    private void UpdateHightScore() {

        float highscore = PlayerPrefs.GetFloat("highscore", 0);

        if (score > highscore) {
            highscore = score;
            PlayerPrefs.SetFloat("highscore", highscore);
        }

        highScoreText.text = Mathf.FloorToInt(highscore).ToString("D5");

    }

}