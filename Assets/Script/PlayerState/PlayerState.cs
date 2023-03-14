using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : IState {

    protected CharacterManager owner;
    protected PlayerStateEnum state;

    public PlayerStateEnum State => state;
    public PlayerState(CharacterManager owner, PlayerStateEnum state) {
        this.owner = owner;
        this.state = state;
    }

    public abstract void Execute();

    public abstract void Exit();

    public abstract void Initialize();
}
