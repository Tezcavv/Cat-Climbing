using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMobile : MonoBehaviour,IController
{


    private float screenPercentage;
 
    private Vector3 firstPos;   //First touch position
    private Vector3 lastPos;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    private Touch touch;

    

    // Start is called before the first frame update
    void Start() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        dragDistance = Screen.width * screenPercentage / 100;
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

        if (!IsDraggedEnough() && GameManager.isGamePaused) {
            GameManager.Pause();
        }
    }
    public Direction GetRotationDirection() {

        if (lastPos.x - firstPos.x > 0) {
            ResetPos();
            return Direction.Right;
        } else {
            ResetPos();
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
        bool result = Mathf.Abs(lastPos.x - firstPos.x) > dragDistance;

        return result;
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
