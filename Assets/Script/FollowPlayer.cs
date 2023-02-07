using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject objToFollow;
    [SerializeField]
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(objToFollow.transform.localPosition + offset, objToFollow.transform.localRotation);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}
