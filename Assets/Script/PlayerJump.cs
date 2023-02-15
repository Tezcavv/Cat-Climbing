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
    private float initialPos;

    RaycastHit hit;

    private bool hasHit;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position.y;
        body= GetComponent<Rigidbody>();
        canJump= true;
    }

    // Update is called once per frame
    void Update()
    {
        ManageJumping();
    }

    private void FixedUpdate() {

    }


    public void Jump() {  

        if(!canJump) return;

        canJump = false;
        Debug.Log(string.Format("Jumping with Velocità: {0} , Posizione Y {1} , HasHit: {2} ", body.velocity, transform.position.y, hasHit));
       
        body.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        
    }

    bool IsFalling() {
        return body.velocity.y < 0;
    }

    void ManageJumping() {

        if (!IsFalling()) {
            return;
        }

       // hasHit = Physics.Raycast(gameObject.transform.position, Vector3.down, rayCastDistance, LayerMask.NameToLayer("Terrain"));
      //  Debug.DrawRay(gameObject.transform.position, Vector3.down * rayCastDistance, Color.red);

        if (transform.position.y == initialPos) {
            canJump = true;
        }

    }


       

    
}
