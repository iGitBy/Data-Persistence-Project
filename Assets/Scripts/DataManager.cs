using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
