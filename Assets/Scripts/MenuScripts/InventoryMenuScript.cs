using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryMenuScript : MonoBehaviour {

	public void ToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
