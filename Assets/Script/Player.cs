using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState { IDLE, RUNNING, JUMPING, MIDDLE_AIR, FALLING }
public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float descentForce;
    Rigidbody body;
    IController controller;
    public PlayerState currentState;

    [SerializeField]
    private float timeInAir = 0f;
    [SerializeField]
    private float FallRayCastDistance;
    [SerializeField]
    private float JumpRayCastDistance;

    private bool hasHit;
    // Start is called before the first frame update
    void Start()
    {
        controller = ControllerFactory.GetController();
        body = GetComponent<Rigidbody>();
        fallBuffered = false;
    }

    // Update is called once per frame
    void Update()
    {
        ManageStates();
    }

    private void FixedUpdate() {

    }


    public void Jump() {  

        body.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        
    }

    bool IsFalling() {
        return body.velocity.y < 0;
    }

    void ManageStates() {

        switch (currentState) {
            case PlayerState.IDLE:
            break;
            case PlayerState.RUNNING:
            UpdateRunning();
            break;
            case PlayerState.JUMPING:
            UpdateJumping();
            break;
            case PlayerState.MIDDLE_AIR:
            UpdateMidAir();
            break;
            case PlayerState.FALLING:
            UpdateFalling();
            break;
        }
    }

    void UpdateMidAir() {

        if (fallBuffered) {
            fallBuffered = false;
            SetToFalling();
            return;
        }

        if (!controller.InputIsValid())
            return;

        if (controller.GetDirection() != Direction.Down)
            return;

        SetToFalling();

    }

    void UpdateRunning() {
        if (!controller.InputIsValid())
            return;

        if (controller.GetDirection() != Direction.Up)
            return;

        Jump();
        currentState = PlayerState.JUMPING;
    }


    private bool fallBuffered;
    void UpdateJumping() {

        if (controller.InputIsValid() && controller.GetDirection() == Direction.Down) {
            fallBuffered = true;
        }

        if(Physics.Raycast(gameObject.transform.position, Vector3.down, JumpRayCastDistance, 1 << 6)) {
            Debug.DrawRay(gameObject.transform.position, Vector3.down * FallRayCastDistance, Color.blue);
            return;
        }

        body.isKinematic = true;
        currentState = PlayerState.MIDDLE_AIR;

        Invoke(nameof(SetToFalling), timeInAir);

    }

    private void SetToFalling() {

        CancelInvoke(nameof(SetToFalling));
        
        body.isKinematic = false;
        body.AddForce(Vector3.down * descentForce,ForceMode.Impulse);
        currentState= PlayerState.FALLING;
    }

    void UpdateFalling() {

        if( !Physics.Raycast(gameObject.transform.position, Vector3.down, FallRayCastDistance, 1 << 6)) { 
            Debug.DrawRay(gameObject.transform.position, Vector3.down * FallRayCastDistance, Color.red);
            return;
        }
        


        currentState = PlayerState.RUNNING;
    }

}
