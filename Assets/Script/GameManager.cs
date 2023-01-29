using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;



public class GameManager : MonoBehaviour
{
    
    private enum Directions {
        Left, Right
    }

    [SerializeField]
    float rotationSpeed;

    // Start is called before the first frame update
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    [Range(1, 40)]
    private int numEsagoni;
    List<GameObject> esagoni;
    private bool isGamePaused;


    GameObject first => esagoni[0];
    GameObject last => esagoni.LastOrDefault();


    private void Start() {

        isGamePaused = false;
        SpawnTerrain();

    }

    private void Update() {
        if (first.transform.position.z <=-70) {
            ResetTerrain();
        }
        if (Input.GetKey(KeyCode.D)) {
            RotateExagon(Directions.Right);
        } else if (Input.GetKey(KeyCode.A)) {
            RotateExagon(Directions.Left);
        }

        ProcessPause();
        ProcessExit();
    }

    void ProcessExit()
    {
        if(Input.GetKey(KeyCode.RightShift)) {
            Application.Quit();
        }
    }

    public void ProcessPause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }

        
    }

    public void PauseUnpause()
    {
        if (!isGamePaused)
        {
            Time.timeScale = 0f;
            isGamePaused = true;
            return;
        }

        Time.timeScale = 1f;
        isGamePaused = false;
        
    }

    void RotateExagon(Directions direction) {

        Vector3 res= Vector3.zero;
        if (direction == Directions.Left) {
            res = Vector3.forward;
        }else if(direction == Directions.Right) {
            res = Vector3.back;
        }
        
        foreach (GameObject esagono in esagoni) {
            esagono.transform.Rotate(res * (rotationSpeed * Time.deltaTime));
        }



    }


    public void SpawnTerrain() {
        esagoni = new List<GameObject>();

        GameObject temp;
        for (int i = 0; i < numEsagoni; i++) {
            temp = Instantiate(prefab, new Vector3(0, 0, i * 30), Quaternion.identity);
            esagoni.Add(temp);
        }
    }

    public void ResetTerrain() {
        first.transform.position = last.transform.position + new Vector3(0,0,30);
        
        TerrainManager terrainManager;

        terrainManager = first.GetComponent<TerrainManager>();
        terrainManager.DeactivateAllObstacles();
        terrainManager.SetObstacles(terrainManager.currentObstacles);
        

        GameObject temp = first;
        esagoni.Remove(first);
        esagoni.Add(temp);

    }


}
