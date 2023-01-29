using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> obstacles;


    public int currentObstacles = 1;
    
    // Start is called before the first frame update
    void Start()
    {
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
        DeactivateAllObstacles();
        SetObstacles(currentObstacles);
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
    
}
