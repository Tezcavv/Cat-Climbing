using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Fall : PlayerState {

    public PlayerState_Fall(CharacterManager owner) : base(owner, PlayerStateEnum.Fall) {
    }

    public override void Initialize() {
        
        owner.transform.DOMoveY(owner.OriginalY, owner.fallTime);
        owner.StartCoroutine(owner.ChangeStateInSecs(PlayerStateEnum.Run, owner.fallTime));
    }
    public override void Execute() {
        
    }

    public override void Exit() {
       
    }


}
