using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    private GameManager gameManager;

    private void Start() {
        gameObject.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
    }
    public void Resume() { //bottone chiama questa funzione
        if (GameManager.Instance.isGamePaused) {
            GameManager.Instance.TogglePause();
        }
    }

    public void Quit() {
        Application.Quit();
    }
}
