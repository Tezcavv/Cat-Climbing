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
    private Vector3 oldPos;
    // Start is called before the first frame update
    void Start()
    {
        body= GetComponent<Rigidbody>();
        canJump= true;
        oldPos= transform.position;
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
        RecordOldPos();
    }

    void RecordOldPos() {
        oldPos= transform.position;
    }

    void Jump() {
        canJump= false;
        body.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
    }

    bool IsFalling() {
        return oldPos.y - transform.position.y > 0;
    }

    void ManageJumping() {

        if (!IsFalling()) {
            return;
        }

        hasHit = Physics.Raycast(gameObject.transform.position, Vector3.down, rayCastDistance, LayerMask.NameToLayer("Terrain"));
        Debug.DrawRay(gameObject.transform.position, Vector3.down * rayCastDistance,Color.red);
        if (hasHit) {
            canJump = true;
        }

    }
}
