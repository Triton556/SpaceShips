using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject soundManager;

    private void Awake()
    {
        DontDestroyOnLoad(soundManager);
    }

    public void LocalStartGame()
    {
        SceneManager.LoadScene("LocalGame");
    }
}
