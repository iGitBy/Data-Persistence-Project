using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] InputField inputName;
    [SerializeField] Text highScoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        //DataManager.Instance.LoadScore();
        DataManager.Instance.LoadLeaderboard();
        DataManager.Instance.LoadSettings();
        //highScoreDisplay.text = "Best Score: " + DataManager.Instance.highScorerName + ": " + DataManager.Instance.highScore;
        highScoreDisplay.text = "Best Score: " + DataManager.Instance.leaderboardRanks[0].highScorerName + ": " + DataManager.Instance.leaderboardRanks[0].highScore;
    }

    // Update is called once per frame
    void Update()
    {
        //TestingTesting();
    }

    public void StartNewGame()
    {
        SubmitName();
        SceneManager.LoadScene(1);
    }

    public void GoToLeaderboard()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene(3);
    }

    public void SubmitName()
    {
       // Debug.Log(inputName.text);

        DataManager.Instance.SaveName(inputName.text);
    }

   /* //Currently tests the SubmitName function upon pressing spacebar in menu scene
    void TestingTesting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SubmitName();
        }
    }*/

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();

#endif
    }

}
