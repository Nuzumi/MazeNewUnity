using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIScript : MonoBehaviour {

    [SerializeField]
    private Text name;
    [SerializeField]
    private Text points;
    [SerializeField]
    private Text level;
    [SerializeField]
    private int playerLevelPerMap;
    [SerializeField]
    private GameObject endGamePanel;

    public GameObject startNextLevelButton;

    private void Start()
    {
        SaveLoadData loadData = SaveLoadDataController.LoadedData;
        int playerLevel = loadData.playerLevel;
        string levelName;

        if(playerLevel == 16)
        {
            endGamePanel.SetActive(true);
        }
        else
        {
            if (playerLevel == 0)
                levelName = "Tutorial";
            else
                levelName = playerLevel + " level";

            startNextLevelButton.GetComponent<Text>().text = "start " + levelName;
            if (loadData.playerName == null)
            {
                name.text = "";
                points.text = "";
                level.text = "";
            }
            else
            {
                name.text += loadData.playerName;
                points.text += loadData.playerPoints;
                level.text += loadData.playerLevel;
            }
        }

    }

    public void OnStartNextLevelClick()
    {
        int playerLevel = SaveLoadDataController.LoadedData.playerLevel;

        if (playerLevel == 0)
        {
            SceneManager.LoadScene("TutorialMap");
        }
        else
        {
            int playerNextMap = (playerLevel / playerLevelPerMap) + 1;
            SceneManager.LoadScene("Map" + playerNextMap);
        }
    }

    public void GoToInventory()
    {
        SceneManager.LoadScene("Inventory");
    }

    public void ResetGame()
    {
        SaveLoadDataController.ClearData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
