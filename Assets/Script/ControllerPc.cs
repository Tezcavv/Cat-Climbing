using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPc : MonoBehaviour,IController
{

    

    void Start() {
       
    }

    // Update is called once per frame
    void Update() {

    }

    public bool InputIsValid() {
        if (Input.GetKeyDown(KeyCode.D)) {
            return true;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            return true; 
        }
        return false;
    }


    public Direction GetRotationDirection() {
        if (Input.GetKeyDown(KeyCode.D)) {
            return Direction.Right;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            return Direction.Left;
        }
        return Direction.Nothing;
    }

    public void ManageExit() {
        if (Input.GetKeyDown(KeyCode.RightShift)) {
            Application.Quit();
        }
    }

    public void ManagePause() {
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            GameManager.Pause();
        }
        
    }

   



}
