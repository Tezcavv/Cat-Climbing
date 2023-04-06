using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerState_Jump : PlayerState {

    
    private float timeInAir;
    public PlayerState_Jump(CharacterManager owner) : base(owner, PlayerStateEnum.Jump) {
        
    }

    public override void Initialize() {

        owner.fallBuffered = false;
        timeInAir= 0;

        owner.transform.DOMoveY(owner.transform.position.y + owner.jumpHeight, owner.jumpTime)
            .SetEase(owner.EaseJump)
            .onComplete += OnComplete; //DELEGATO, muy guapo


        owner.animator.Play("Jump");
        
    }

    private void OnComplete() {
        owner.ChangeState(PlayerStateEnum.Fall);
    }

    public override void Execute() {

        timeInAir += Time.deltaTime;

        CheckForBuffer();

        if (timeInAir <= owner.heigthLimit)
            //non ha raggiunto nemmeno l'altezza minima per scendere
            return;

        if (!owner.fallBuffered )
            //ha raggiunto l'altezza minima (non la massima)
            //non è stato dato il comando di scendere
            return;

        if (owner.fallBuffered || timeInAir >= owner.jumpTime) {
            owner.ChangeState(PlayerStateEnum.Fall);
        }
    }

    private void CheckForBuffer() {
        if (controller.InputIsValid() &&
            controller.GetDirection() == Direction.Down)

            owner.fallBuffered = true;
            
    }

    public override void Exit() {
        //non va reso falso il fallBuffered perché servirà al FALL
        owner.transform.DOKill(false);
            
    }

   


}
