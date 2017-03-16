using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * A Controller for the Start Menu. This script waits for the user to press the
 * Spacebar. Once they do, the 'Game' scene is loaded and the gameplay begins.
 */
public class StartController : MonoBehaviour {
    // Reads user input. Only responds to a Spacebar press.
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
        }
	}
}
