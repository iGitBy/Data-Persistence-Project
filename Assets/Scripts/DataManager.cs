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

    [System.Serializable]
    class SaveData
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
}
