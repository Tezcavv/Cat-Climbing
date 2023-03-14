using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Jump : PlayerState {


    public PlayerState_Jump(CharacterManager owner) : base(owner, PlayerStateEnum.Jump) {
    }

    public override void Initialize() {
        
        owner.transform.DOMoveY(owner.transform.position.y + owner.jumpHeight, owner.jumpTime);
        owner.StartCoroutine(owner.ChangeStateInSecs(PlayerStateEnum.Fall, owner.jumpTime));

    }

    public override void Execute() {

        
    }

    public override void Exit() {
       
    }


}
