using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIScript : MonoBehaviour {

    [SerializeField]
    private int playerLevelPerMap;
    public GameObject startNextLevelButton;

    private void Start()
    {
        int playerLevel = SaveLoadDataController.LoadedData.playerLevel;
        string levelName;
        if (playerLevel == 0)
            levelName = "Tutorial";
        else
            levelName = playerLevel + " level";

        startNextLevelButton.GetComponent<Text>().text = "start " + levelName;
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

}
