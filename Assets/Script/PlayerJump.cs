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

    void Jump() {
        canJump= false;
        body.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            canJump= true;
        }
    }
}
