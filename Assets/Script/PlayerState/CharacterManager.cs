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

    [SerializeField] private Ease easeJump;
    [SerializeField] private Ease easeFall;
    public Ease EaseJump => easeJump;
    public Ease EaseFall => easeFall;

    #region Fields
    private float originalY;
    [Tooltip("Altezza del salto")]
    public float jumpHeight = 2f;
    [Tooltip("Tempo di salto")]
    public float jumpTime = 2f;
    [Tooltip("Tempo di Discesa")]
    public float fallTime = 2f;
    [Tooltip("Altezza minima per poter effettuare la discesa anticipata")]
    public float heigthLimit = 2f;
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
        animator = GetComponent<Animator>();
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


    // Update is called once per frame
    void Update() {
        stateMachine.Execute();
    }

}
