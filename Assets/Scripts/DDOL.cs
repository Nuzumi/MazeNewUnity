using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour {

    [SerializeField]
    private bool goToMenuOnStart;
    private static bool created;

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }
    }

    private void Start()
    {
        if(goToMenuOnStart)
            SceneManager.LoadScene("Menu");
    }
}
