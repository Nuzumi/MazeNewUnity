using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIScript : MonoBehaviour {

    public GameObject startNextLevelButton;

    

    private void Start()
    {
        startNextLevelButton.GetComponent<Text>().text = "start 1 level";
    }

    public void OnStartNextLevelClick()
    {
        SceneManager.LoadScene("Level1");
    }

}
