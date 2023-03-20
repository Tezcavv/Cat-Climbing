using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject objToFollow;
    [SerializeField]
    Vector3 offsetPosition;
    [SerializeField]
    Vector3 offsetRotation;

    private Vector3 truePosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = objToFollow.transform.position + offsetPosition;
        truePosition = objToFollow.transform.position;
        transform.DORotate(offsetRotation, 0f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = truePosition + offsetPosition;
        transform.DORotate(offsetRotation,0f);
    }
}
