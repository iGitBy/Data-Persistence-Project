using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] InputField inputName;
    [SerializeField] Text highScoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        highScoreDisplay.text = "Best Score: " + DataManager.Instance.highScorerName + ": " + DataManager.Instance.highScore;
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

}
