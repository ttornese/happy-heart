﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsContoller : MonoBehaviour {
    public GameObject pauseMenu;
	public GameObject gameOverMenu;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

	public void RestartGame()
	{
		gameOverMenu.SetActive (false);
        SceneManager.LoadScene("Game");
	}
}

