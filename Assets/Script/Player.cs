using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState { IDLE, RUNNING, JUMPING, MIDDLE_AIR, FALLING }
public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;
    Rigidbody body;
    IController controller;
    public PlayerState currentState;

    [SerializeField]
    private float timeInAir = 0f;
    [SerializeField]
    private float rayCastDistance = 1f;

    private bool hasHit;
    // Start is called before the first frame update
    void Start()
    {
        controller = ControllerFactory.GetController();
        body = GetComponent<Rigidbody>();
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

        if (!controller.InputIsValid())
            return;

        if (controller.GetDirection() != Direction.Down)
            return;

        Debug.Log("Adesso Scendo");
        currentState = PlayerState.FALLING;

    }

    void UpdateRunning() {
        if (!controller.InputIsValid())
            return;

        if (controller.GetDirection() != Direction.Up)
            return;

        Jump();
        currentState = PlayerState.JUMPING;
    }

    void UpdateJumping() {

        if (body.velocity.y >= 0)
            return;

        body.isKinematic = true;
        Invoke(nameof(SetToFalling), timeInAir);
        currentState = PlayerState.MIDDLE_AIR;

    }

    private void SetToFalling() {
        body.isKinematic = false;
        currentState= PlayerState.FALLING;
    }

    void UpdateFalling() {

        hasHit = Physics.Raycast(gameObject.transform.position, Vector3.down, rayCastDistance, 1 << 6);
        Debug.DrawRay(gameObject.transform.position, Vector3.down * rayCastDistance, Color.red);

        if (!hasHit)
            return;

        currentState = PlayerState.RUNNING;
    }

}
