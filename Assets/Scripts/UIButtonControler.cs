using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonControler : MonoBehaviour {
    
    public GameObject playerCamera;
    public GameObject player;
    
    

    public void ShowWholeMazeButton()
    {
        if (playerCamera.activeSelf)
        {
            playerCamera.SetActive(false);
            player.GetComponent<PlayerControler>().CanMove = false;
        }
        else
        {
            playerCamera.SetActive(true);
            player.GetComponent<PlayerControler>().CanMove = true;
        }

    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
