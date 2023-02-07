using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    
    private IController controller;

    [SerializeField]
    float mobileScreenPercentage;
    [SerializeField]
    float jumpTime;
    [SerializeField]
    GameObject exagonPrefab;
    [SerializeField]
    [Range(1, 40)]
    private int numEsagoni;
    public float chosenRotation = 60f;
    List<GameObject> esagoni;
    public static bool isGamePaused;
    private bool canJump = true;
    GameObject First => esagoni[0];
    GameObject Last => esagoni.LastOrDefault();

    private void Start() {
        isGamePaused= false;
        jumpTime = 1.0f;

        if (SystemInfo.deviceType == DeviceType.Desktop) {
            controller = gameObject.AddComponent<ControllerPc>();
        } else {
            controller = gameObject.AddComponent<ControllerMobile>();
            GetComponent<ControllerMobile>().ScreenPercentage=mobileScreenPercentage;
        }

        SpawnTerrain();

    }

    private void Update() {
        if (First.transform.position.z <= -70) {
            ResetTerrain();
        }

        if (controller.InputIsValid()) {
            Direction dir = controller.GetRotationDirection();
            RotateExagon(dir);
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

    public static void Pause() {

        if (!isGamePaused) {
            Time.timeScale = 0f;
            isGamePaused = true;
            return;
        }

        Time.timeScale = 1f;
        isGamePaused = false;
        
    }

    public void RotateExagon(Direction dir) {

        if (!canJump) {
            return;
        }
        canJump = false;
        float zDestination = 0;
        int direction = 0;
        Vector3 destination;

        if (dir == Direction.Left) {
            direction = 1;
        } else if (dir == Direction.Right) {
            direction = -1;
        }
        Debug.Log("Muovo in direzione");
        foreach (GameObject esagono in esagoni) {

            zDestination = esagono.transform.rotation.eulerAngles.z + ( chosenRotation * direction );
            Debug.Log("From " + esagono.transform.rotation.z + " to " + zDestination);
            destination = new Vector3(0, 0, zDestination);
            esagono.transform.DORotate(destination, jumpTime,RotateMode.FastBeyond360);
        }     
        Invoke("Cooldown", jumpTime - 0.3f);
    }

    void Cooldown() {
        canJump = true;
    }

}
