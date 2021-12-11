using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private GameObject gameOverBackground;

    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    //[SerializeField] private float speed = 3.0f; // og was 2
    
    // Start is called before the first frame update
    void Start()
    {
        ScoreboardUpdate();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * DataManager.Instance.startSpeed, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        
    }

    public void GameOver()
    {
        //DataManager.Instance.CheckForNewHighScore(m_Points);
        DataManager.Instance.CheckForRank(m_Points);
        ScoreboardUpdate();
        //DataManager.Instance.SaveScore();
        DataManager.Instance.SaveLeaderboard();
        m_GameOver = true;
        GameOverText.SetActive(true);
        returnToMenuButton.gameObject.SetActive(true);
        gameOverBackground.gameObject.SetActive(true);
    }

    public void ScoreboardUpdate()
    {
        //BestScoreText.text = "Best Score: " + DataManager.Instance.highScorerName + ": " + DataManager.Instance.highScore;
        BestScoreText.text = "Best Score: " + DataManager.Instance.leaderboardRanks[0].highScorerName + ": " + DataManager.Instance.leaderboardRanks[0].highScore;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
