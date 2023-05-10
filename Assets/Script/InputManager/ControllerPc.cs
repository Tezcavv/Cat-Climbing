using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ControllerPc: MonoBehaviour,IController {

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }
    public bool InputIsValid() {
        if (Input.GetKeyDown(KeyCode.D)) {
            return true;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            return true; 
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            return true;
        }else if (Input.GetKeyDown(KeyCode.S)) {
            return true;
        }
        return false;
    }


    public Direction GetDirection() {
        if (Input.GetKeyDown(KeyCode.D)) {
            return Direction.Right;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            return Direction.Left;
        }else if (Input.GetKeyDown(KeyCode.Space)) {
            return Direction.Up;
        }else if (Input.GetKeyDown(KeyCode.S))
            return Direction.Down;
        return Direction.Nothing;
    }

    public void ManageExit() {
        if (Input.GetKeyDown(KeyCode.RightShift)) {
            Application.Quit();
        }
    }

    public void ManagePause() {
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            GameManager.Instance.TogglePause();
        }
        
    }

   



}
