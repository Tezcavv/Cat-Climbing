using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField]
    private GameObject obstacle;
    [SerializeField]
    private int numberOfOstacles;
    private List<GameObject> obstacles;
    private float randomNum;
    // Start is called before the first frame update
    void Start()
    {
        obstacles = new List<GameObject>();
        for(int i = 0; i < numberOfOstacles; i++)
        {
            SpawnObstacles();
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject ob in obstacles)
        {
            ob.transform.SetPositionAndRotation(transform.position + new Vector3(0, 1, 0), transform.rotation);
        }
    }

    public void SpawnObstacles() {

        GameObject temp = GameObject.Instantiate(obstacle);
        obstacles.Add(temp);

    }
}
