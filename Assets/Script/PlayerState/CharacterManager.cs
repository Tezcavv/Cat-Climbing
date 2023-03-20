using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public enum PlayerStateEnum { Run, Jump, Fall}

public class CharacterManager : MonoBehaviour {

    #region StateMachine
    public StateMachine stateMachine;
    public IController inputController;
    public List<IState> states;
    #endregion

    #region Fields
    private float originalY;
    public float jumpHeight = 2f;
    public float jumpTime = 2f;
    public float fallTime = 2f;
    public float midAirTime = 1f;
    public bool fallBuffered = false;
    public Animator animator;
    #endregion

    #region Properties
    public float OriginalY { get { return originalY; } }
    #endregion



    private void Start() {
      
        originalY = transform.position.y;

        stateMachine = new StateMachine();
        inputController = ControllerFactory.Instance;
        states = new List<IState> {
            new PlayerState_Run(this),
            new PlayerState_Jump(this),
            new PlayerState_Fall(this)
        };

        ChangeState(PlayerStateEnum.Run);
    }

    public void ChangeState(PlayerStateEnum state) {
        foreach (PlayerState ps in states) {
            if (ps.State == state) {
                stateMachine.ChangeState(ps);
                return;
            }
        }
    }

    public IEnumerator ChangeStateInSecs(PlayerStateEnum state, float secDelay) {
        yield return new WaitForSeconds(secDelay);
        ChangeState(state);
    }


    // Update is called once per frame
    void Update() {
        stateMachine.Execute();
    }

}
