using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction { Left, Right, Up , Nothing }
public interface IController {

    bool InputIsValid();
    Direction GetDirection();
    void ManagePause();

    void ManageExit();

}