using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;
    Rigidbody body;
    public bool canJump;
    [SerializeField]
    private float rayCastDistance;

    RaycastHit hit;

    private bool hasHit;
    // Start is called before the first frame update
    void Start()
    {
        body= GetComponent<Rigidbody>();
        canJump= true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && canJump) {
            Jump();
        }
    }

    private void FixedUpdate() {
        ManageJumping();
    }


    void Jump() {
        
        canJump= false;
        body.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        
        
    }

    bool IsFalling() {
        return body.velocity.y < 0;
    }

    void ManageJumping() {

        if (!IsFalling()) {
            return;
        }

        hasHit = Physics.Raycast(gameObject.transform.position, Vector3.down, rayCastDistance, LayerMask.NameToLayer("Terrain"));
        Debug.DrawRay(gameObject.transform.position, Vector3.down * rayCastDistance,Color.red);
        if (hasHit) {
            Debug.Log("I Hit Terrain");
            canJump = true;
        }

    }
}
