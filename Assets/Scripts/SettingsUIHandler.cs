using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsUIHandler : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] Text selectedDifficultyText;


    // Start is called before the first frame update
    void Start()
    {
        
        dropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(dropdown); });
        dropdown.value = DataManager.Instance.difficultyLevel;
        DropdownValueChanged(dropdown);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DropdownValueChanged(Dropdown change)
    {
        string textDisplay = "- select difficulty - ";

       
        if (change.value == 0)
        {
            textDisplay = "EASY";
            DataManager.Instance.EasyMode();
        }
        else if(change.value == 1)
        {
            textDisplay = "NORMAL";
            DataManager.Instance.NormalMode();
        }
        else if(change.value == 2)
        {
            textDisplay = "HARD";
            DataManager.Instance.HardMode();
        }

        selectedDifficultyText.text = "Difficulty: " + textDisplay;
        DataManager.Instance.SaveSettings();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
