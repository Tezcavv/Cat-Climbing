using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField]
    private float speed;

    public float Speed { get { return speed; } set { speed = value; } }

    // Start is called before the first frame update
    private void Start() {
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += GameManager.Instance.GameSpeed * Time.deltaTime * speed * Vector3.back;
    }
}
