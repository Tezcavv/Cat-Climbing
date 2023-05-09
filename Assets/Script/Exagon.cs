using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Exagon : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> levels;

    public float chosenRotation = 60f;
    private Vector3 oldRotation;
    private Vector3 newRotation;
    private float dodgeTime;

    public Vector3 OldRotation => oldRotation;
    public Vector3 NewRotation => newRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        dodgeTime = GameManager.Instance.sidewaysJumpDuration;
        newRotation = transform.rotation.eulerAngles;
        oldRotation = transform.rotation.eulerAngles;

        DeactivateAlllevels();
        PickALevel();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeactivateAlllevels()
    {
        foreach(GameObject level in levels)
        {
            level.SetActive(false);
        }
    }

    public void PickALevel() {

        int random = Random.Range(0, levels.Count);
        levels[random].SetActive(true);
       
    }



    public void Rotate(Direction dir) {

        float zDestination;
        int direction = 0;

        if (dir == Direction.Left) {
            direction= 1;
        } else if (dir == Direction.Right) {
            direction= -1;
        }

        oldRotation = new Vector3(newRotation.x,newRotation.y,newRotation.z);

        zDestination = oldRotation.z + (chosenRotation * direction);
        newRotation = new Vector3(0, 0, zDestination);

        transform.DORotate(newRotation, jumpTime*GameManager.Instance.GameSpeed, RotateMode.Fast);

    }
    
}
