using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderboardHandler : MonoBehaviour
{
    [SerializeField] List<Text> leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        RefreshLeaderboard();
    }

    // Update is called once per frame
    void Update()
    {
        if (DataManager.Instance.deletedSaveData)
        {
            RefreshLeaderboard();
        }
    }

    void RefreshLeaderboard()
    {
        /*for (int i = 0; i < leaderboard.Count; i++)
        {
            leaderboard[i].text = "#" + (i + 1) + " -empty- : " + "0";
        }*/

        for (int i = 0; i < DataManager.Instance.leaderboardRanks.Count; i++)
        {
            leaderboard[i].text = "#" + (i + 1) + " " + DataManager.Instance.leaderboardRanks[i].highScorerName + ": " + DataManager.Instance.leaderboardRanks[i].highScore;
        }


    }

    public void Delete()
    {
        //DataManager.Instance.DeleteSaveData();
        DataManager.Instance.DeleteLeaderboard();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
