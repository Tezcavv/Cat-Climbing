using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{
    private GameManager gameManager;

    private void Start() {
        gameObject.SetActive(false);
        gameManager = GameManager.Instance;
    }
    public void Resume() { //bottone chiama questa funzione
        if (gameManager.isGamePaused) {
            gameManager.TogglePause();
        }
    }

    public void Quit() {
        Application.Quit();
    }

    public void ToMainMenu() {
        Resume();
        SceneManager.LoadScene(0);
        
    }
}
