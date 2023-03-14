using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : IState {

    protected CharacterManager owner;
    protected PlayerStateEnum state;
    protected IController controller;

    public PlayerStateEnum State => state;
    public PlayerState(CharacterManager owner, PlayerStateEnum state) {
        this.owner = owner;
        this.state = state;
        controller = ControllerFactory.Instance;
    }

    public abstract void Execute();

    public abstract void Exit();

    public abstract void Initialize();

}
