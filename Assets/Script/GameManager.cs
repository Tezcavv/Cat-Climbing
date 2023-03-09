using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    
    IController controller;

    [SerializeField]
    float mobileScreenPercentage;
    [SerializeField]
    GameObject exagonPrefab;
    [SerializeField]
    [Range(1, 40)]
    private int numEsagoni;
    public float chosenRotation = 60f;
    List<GameObject> esagoni;
    public bool isGamePaused;
    private bool canRotate = true;
    public GameObject First => esagoni[0];
    GameObject Last => esagoni.LastOrDefault();
    [SerializeField]
    float jumpTime;
    [Range(0, 2)]
    public float rotationCooldown;
    private Vector3 destination;
    public Player player;
    public float maxHeightForMovement;

    public static GameManager Instance { get; private set; }

    [SerializeField]
    private UIPauseMenu pauseMenu;

    private void Awake() {
        Instance= this;
    }

    private void Start() {

        destination= transform.rotation.eulerAngles;

        isGamePaused= false;
        //TOFIX
        Time.timeScale= 1.0f;

        controller = ControllerFactory.GetController();

        SpawnTerrain();

    }

    private void Update() {
        //update GameManager
        if (First.transform.position.z <= -70) {
            ResetTerrain();
        }

        if (controller.InputIsValid() ) {
            //Qui riesce a entrare solo quando PlayerState.FALLING
            Direction dir = controller.GetDirection();
            if(dir == Direction.Left || dir == Direction.Right) {
                RotateExagon(dir);
            }
            
        }

        controller.ManagePause();
        controller.ManageExit();
    }


    public void SpawnTerrain() {
        esagoni = new List<GameObject>();

        GameObject temp;
        for (int i = 0; i < numEsagoni; i++) {
            temp = Instantiate(exagonPrefab, new Vector3(0, 0, i * 30), Quaternion.identity);
            esagoni.Add(temp);
        }
    }

    public void ResetTerrain() {
        First.transform.position = Last.transform.position + new Vector3(0, 0, 30);

        TerrainManager terrainManager;

        terrainManager = First.GetComponent<TerrainManager>();
        terrainManager.DeactivateAllObstacles();
        terrainManager.SetObstacles(terrainManager.currentObstacles);


        GameObject temp = First;
        esagoni.Remove(First);
        esagoni.Add(temp);

    }

    public void TogglePause() {

        if (!isGamePaused) {
            Time.timeScale = 0f;
            isGamePaused = true;
            pauseMenu.gameObject.SetActive(true);
            return;
        }
        
        Time.timeScale = 1f;
        isGamePaused = false;
        pauseMenu.gameObject.SetActive(false);

    }

    public void RotateExagon(Direction dir) {

        if (!canRotate)
            return;
        //        if (player.currentState != PlayerState.RUNNING) 
        //  return;
        
        canRotate = false;
        float zDestination;
        int direction = 0;

        if (dir == Direction.Left) {
            direction = 1;
        } else if (dir == Direction.Right) {
            direction = -1;
        }

        
        zDestination = destination.z + (chosenRotation * direction);
        destination = new Vector3(0, 0, zDestination);
        foreach (GameObject esagono in esagoni) {
           
            esagono.transform.DORotate(destination, jumpTime,RotateMode.Fast);
        }     
        Invoke(nameof(Cooldown), rotationCooldown);
    }

    void Cooldown() {
        canRotate = true;
    }

}
