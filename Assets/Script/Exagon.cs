using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Exagon : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> obstacles;

    public float chosenRotation = 60f;
    public int currentObstacles = 1;
    private Vector3 destination;
    public float jumpTime;

    
    // Start is called before the first frame update
    void Start()
    {
        //era attaccato al GameManager
        destination = transform.rotation.eulerAngles;

        InitializeObstacles();
        DeactivateAllObstacles();
        SetObstacles(currentObstacles);
    }

    void InitializeObstacles() {
        obstacles = new List<GameObject>();
        List<GameObject> listChildren = GetDirectChildren(gameObject);

        foreach (GameObject plane in listChildren)
        {
            List<GameObject>listChildr2= GetDirectChildren(plane);

            foreach (GameObject obstacle in listChildr2)
            {
                obstacles.Add(obstacle);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeactivateAllObstacles()
    {
        foreach(GameObject go in obstacles)
        {
            go.SetActive(false);
        }
    }

    public void SetObstacles(int numberOfObstacles) {

        int numOfObstacles = obstacles.Count;
        int randomIndex;

         for (int i = 0; i < numberOfObstacles; i++)
         {
            randomIndex = Random.Range(0, numOfObstacles);
            obstacles[randomIndex].SetActive(true);
         }

    }

    private List<GameObject> GetDirectChildren(GameObject gameObject)
    {
        List<GameObject> result = new List<GameObject>();
        
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject gameObjectToAdd = gameObject.transform.GetChild(i).gameObject;
            result.Add(gameObjectToAdd);
        }
        return result;
    }


    public void Rotate(Direction dir) {

        float zDestination;
        int direction = 0;

        if (dir == Direction.Left) {
            direction= 1;
        } else if (dir == Direction.Right) {
            direction= -1;
        }

        zDestination = destination.z + (chosenRotation * direction);
        destination = new Vector3(0, 0, zDestination);

        transform.DORotate(destination, jumpTime, RotateMode.Fast);


    }
    
}
