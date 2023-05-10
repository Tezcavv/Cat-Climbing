using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField]
    private float speed;
    public Vector3 lastPos= Vector3.zero;

    public float Speed { get { return speed; } set { speed = value; } }

    // Start is called before the first frame update
    private void Start() {
    }
    // Update is called once per frame
    void Update()
    {
        lastPos= transform.position;
        transform.position += GameManager.Instance.GameSpeed * Time.deltaTime * speed * Vector3.back;
    }
}
