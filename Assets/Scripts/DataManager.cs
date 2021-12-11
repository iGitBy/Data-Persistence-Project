using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string newName;

    public string highScorerName = "-none-";

    public int highScore = 0;

    public int numberOfRanks = 10;

    public int difficultyLevel; // 0 is Easy, 1 is Normal, and 2 is HARD -- find in SettingsUIHandler

    public bool deletedSaveData = false;

    public List<SaveData> leaderboardRanks;

    //Difficulty Settings

    [SerializeField] public float maximumVelocity;
    [SerializeField] public float startSpeed;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);

           //initialize leaderboard values
            for(int i=0; i<numberOfRanks; i++)
            {
                SaveData rank = new SaveData();
                rank.highScore = 0;
                rank.highScorerName = " - empty - ";
                leaderboardRanks.Add(rank);
            }

            //start at NORMAL difficulty
            difficultyLevel = 1;
        }
    }

    public void SaveName(string nameToSave)
    {
        newName = nameToSave;
    }

    public void CheckForNewHighScore(int newScore)
    {
        if(newScore > highScore)
        {
            highScore = newScore;
            highScorerName = newName;
        }

    }

    public void CheckForRank(int newScore)
    {
        for(int i = 0; i<leaderboardRanks.Count; i++)
        {
            if(newScore >= leaderboardRanks[i].highScore)
            {
                SaveData newRank = new SaveData();
                newRank.highScore = newScore;
                newRank.highScorerName = newName;
                leaderboardRanks.Insert(i, newRank);

                //make sure to bump the last-place score off the leaderboard!
                leaderboardRanks.RemoveAt(10);

                return;
            }
        }

    }

    [System.Serializable]
    public class SaveData
    {
        public int highScore;
        public string highScorerName;
    }


    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.highScorerName = highScorerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void SaveLeaderboard()
    {
        for(int i = 0; i < leaderboardRanks.Count; i++)
        {
            SaveData data = new SaveData();
            data.highScore = leaderboardRanks[i].highScore;
            data.highScorerName = leaderboardRanks[i].highScorerName;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile" + i + ".json", json);
        }
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            highScorerName = data.highScorerName;
        }
        else
        {
            return;
        }
    }

    public void LoadLeaderboard()
    {
        for (int i = 0; i < leaderboardRanks.Count; i++)
        {
            string path = Application.persistentDataPath + "/savefile" + i + ".json";

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);

                SaveData data = JsonUtility.FromJson<SaveData>(json);

                leaderboardRanks[i].highScore = data.highScore;
                leaderboardRanks[i].highScorerName = data.highScorerName;
            }
        }

    }

    public void DeleteSaveData()
    {
        highScore = 0;
        highScorerName = " - empty - ";
        deletedSaveData = true;


        SaveData data = new SaveData();
        data.highScore = highScore;
        data.highScorerName = highScorerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void DeleteLeaderboard()
    {
        deletedSaveData = true;

        for(int i = 0; i < leaderboardRanks.Count; i++)
        {
            leaderboardRanks[i].highScore = 0;
            leaderboardRanks[i].highScorerName = " - empty - ";
        }


        for (int i = 0; i < leaderboardRanks.Count; i++)
        {
            SaveData data = new SaveData();
            data.highScore = leaderboardRanks[i].highScore;
            data.highScorerName = leaderboardRanks[i].highScorerName;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile" + i + ".json", json);
        }
    }

    void DebugTester()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("TESTING");
        }
    }

    //--------DIFFICULTY SETTINGS-----------

    public void EasyMode()
    {
        difficultyLevel = 0;
        maximumVelocity = 2;
        startSpeed = 1;
    }

    public void NormalMode()
    {
        difficultyLevel = 1;
        maximumVelocity = 3;
        startSpeed = 2;
    }

    public void HardMode()
    {
        difficultyLevel = 2;
        maximumVelocity = 4;
        startSpeed = 3;
    }

    [System.Serializable]
    public class SettingsData
    {
        public int difficulty;
    }

    public void SaveSettings()
    {
        SettingsData data = new SettingsData();
        data.difficulty = difficultyLevel;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefileSETTINGS.json", json);
    }

    public void LoadSettings()
    {
        string path = Application.persistentDataPath + "/savefileSETTINGS.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SettingsData data = JsonUtility.FromJson<SettingsData>(json);

            difficultyLevel = data.difficulty;
        }
    }



}
