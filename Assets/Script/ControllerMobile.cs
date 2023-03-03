using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMobile : MonoBehaviour,IController
{

    private float screenPercentage = 10f;
 
    private Vector3 firstPos;   //First touch position
    private Vector3 lastPos;   //Last touch position
    private float dragDistanceX;  //minimum distance for a swipe to be registered
    private float dragDistanceY;
    private Touch touch;

    

    // Start is called before the first frame update
    void Start() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 144;
        dragDistanceX = Screen.width * screenPercentage / 100f;
        dragDistanceY = Screen.height * screenPercentage / 100f;
    }

    // Update is called once per frame
    void Update() {
        UpdateTouch();
    }

    public bool InputIsValid() {
        return IsDraggedEnough();
    }

    void UpdateTouch() {

        if (Input.touchCount != 1) {
            ResetPos();
            return;
        }

        touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began) {
            firstPos = touch.position;
            lastPos = touch.position;
            return;
        }
        if (touch.phase == TouchPhase.Moved) {
            lastPos = touch.position;
            return;
        }
        if (touch.phase != TouchPhase.Ended) {
            return;
        }
        lastPos = touch.position;

        if (!IsDraggedEnough() && GameManager.Instance.isGamePaused) {
            GameManager.Instance.TogglePause();
        }
    }
    public Direction GetDirection() {

        if(lastPos.y - firstPos.y > 0 && lastPos.y - firstPos.y >= dragDistanceY) { 
            return Direction.Up;
        }

        if (lastPos.y - firstPos.y < 0 && Mathf.Abs(lastPos.y - firstPos.y) >= dragDistanceY) {
            return Direction.Down;
        }

        if (lastPos.x - firstPos.x > 0) {
            return Direction.Right;
        } else {
            return Direction.Left;
        }
    }

    private void ResetPos() {
        lastPos = firstPos;
    }

    public void ManageExit() {
        
    }

    public void ManagePause() {
        
    }


    private bool IsDraggedEnough() {

        if( Mathf.Abs(lastPos.x - firstPos.x) >= dragDistanceX) {
            return true;
        }
        if (Mathf.Abs(lastPos.y - firstPos.y) >= dragDistanceY) {
            return true;
        }

        return false;
    }

    public float ScreenPercentage {
        get { return screenPercentage; }
        set { 
            if(value<0) { return; }
            else if(value >=100) { return; }
            screenPercentage = value; 
        }
    }


}
