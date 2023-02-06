using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction { Left, Right, Nothing }
public interface IController {

    bool InputIsValid();
    Direction GetRotationDirection();
    void ManagePause();

    void ManageExit();

}