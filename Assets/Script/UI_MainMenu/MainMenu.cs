using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


   public void StartGame() {
        //load game
        SceneManager.LoadScene(1);

   }

    public void OpenWindow(GameObject gameObjectToOpen) {
        gameObjectToOpen.SetActive(true);
    }

    public void CloseWindow(GameObject gameObjectToDisable) {
        gameObjectToDisable.SetActive(false);
    }

    
}
