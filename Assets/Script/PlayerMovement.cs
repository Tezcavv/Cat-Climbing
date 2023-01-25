using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)) {

            Vector3 target = new Vector3(transform.position.x + 5, transform.position.y + 3, transform.position.z);
            Vector3.MoveTowards(transform.position, target, 1);
            transform.rotation = Quaternion.Euler(0,0,-60);

  
        }
    }
}
