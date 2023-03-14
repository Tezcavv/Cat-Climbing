using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Run : PlayerState {
    public PlayerState_Run(CharacterManager owner) : base(owner, PlayerStateEnum.Run) {
    }

    public override void Initialize() {
        
    }
    public override void Execute() {

        if (owner.inputController.GetDirection() == Direction.Up)
            owner.ChangeState(PlayerStateEnum.Jump);

    }

    public override void Exit() {

    }

}
