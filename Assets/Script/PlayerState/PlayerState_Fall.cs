using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Fall : PlayerState {

    public PlayerState_Fall(CharacterManager owner) : base(owner, PlayerStateEnum.Fall) {
    }

    public override void Initialize() {
        if (owner.fallBuffered) {
            owner.transform.DOMoveY(owner.OriginalY, owner.fallTime/2).SetEase(owner.EaseFall).onComplete += OnComplete;
        } else {
            owner.transform.DOMoveY(owner.OriginalY, owner.fallTime).SetEase(owner.EaseFall).onComplete += OnComplete;
        }

        owner.animator.SetTrigger("DodgeFall");

        //owner.StartCoroutine(owner.ChangeStateInSecs(PlayerStateEnum.Run, owner.fallTime));

    }

    private void OnComplete() {
        owner.ChangeState(PlayerStateEnum.Run);
    }

    public override void Execute() {
        
    }

    public override void Exit() {
       
    }


}
