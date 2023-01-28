using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> obstacles;

    public List<GameObject> ObstaclesList => obstacles;

    public int currentObstacles = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        DeactivateAllObstacles();
        SetObstacles(currentObstacles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeactivateAllObstacles()
    {
        foreach(GameObject go in ObstaclesList)
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
}
