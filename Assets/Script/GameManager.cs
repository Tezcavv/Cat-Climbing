using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;



public class GameManager : MonoBehaviour {

    private enum Directions {
        Left, Right
    }

    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float rotationTime = 1.0f;

    // Start is called before the first frame update
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    [Range(1, 40)]
    private int numEsagoni;
    List<GameObject> esagoni;
    private bool isGamePaused;

    private Vector3 firstPos;   //First touch position
    private Vector3 lastPos;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    private Touch touch;

    public enum Axis { X, Y }

    GameObject First => esagoni[0];
    GameObject Last => esagoni.LastOrDefault();


    private void Start() {

        isGamePaused = false;
        SpawnTerrain();
        if(IsOnMobile()) {
            dragDistance = Screen.height * 15 / 100;
        }

    }

    private void Update() {
        if (First.transform.position.z <= -70) {
            ResetTerrain();
        }

        ProcessControls();

        if(IsOnPc()) {
            ProcessPause();
            ProcessExit();
        }
        
    }

    void ProcessControlsOnMobile() {

        if (Input.touchCount != 1) {
            return;
        }

        touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began) {
            firstPos = touch.position;
            lastPos = touch.position;
            return;
        }
        if (touch.phase == TouchPhase.Moved) {
            lastPos = touch.position;
            return;
        }
        if (touch.phase != TouchPhase.Ended) {
            return;
        }
        lastPos = touch.position;

        if (!isDraggedEnough(Axis.X) ) {
            return;
        }

        StartCoroutine(RotateObject());

    }

    IEnumerator RotateObject() {
        float elapsedTime = 0.0f;
        float targetAngle = 360.0f;
        float startAngle = transform.eulerAngles.y;
        float angle = 0.0f;

        while (elapsedTime < rotationTime) {
            elapsedTime += Time.deltaTime;
            angle = Mathf.Lerp(startAngle, targetAngle, elapsedTime / rotationTime) % 360;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
            yield return null;
        }
    }

    private bool isDraggedEnough(Axis ax) {
        if (ax == Axis.X) {
            return Mathf.Abs(lastPos.x - firstPos.x) > dragDistance;
        }
        if (ax == Axis.Y) {
            return Mathf.Abs(lastPos.y - firstPos.y) > dragDistance;
        }

        return false;

    }


    void ProcessControls() {

        if (IsOnPc()) {
            ProcessControlsOnPc();
            return;
        }
        if (IsOnMobile()) {
            ProcessControlsOnMobile();
            return;
        }

    }

    void ProcessControlsOnPc() {

        if (Input.GetKey(KeyCode.D)) {
            RotateExagon(Directions.Right);
        }
        else if (Input.GetKey(KeyCode.A)) {
            RotateExagon(Directions.Left);
        }
    }


    void ProcessExit() {

        

        if (Input.GetKey(KeyCode.RightShift)) {
            Application.Quit();
        }
    }

    public void ProcessPause() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseUnpause();
        }


    }

    public void PauseUnpause() {
        if (!isGamePaused) {
            Time.timeScale = 0f;
            isGamePaused = true;
            return;
        }

        Time.timeScale = 1f;
        isGamePaused = false;

    }

    void RotateExagon(Directions direction) {

        Vector3 res = Vector3.zero;
        if (direction == Directions.Left) {
            res = Vector3.forward;
        }
        else if (direction == Directions.Right) {
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
        First.transform.position = Last.transform.position + new Vector3(0, 0, 30);

        TerrainManager terrainManager;

        terrainManager = First.GetComponent<TerrainManager>();
        terrainManager.DeactivateAllObstacles();
        terrainManager.SetObstacles(terrainManager.currentObstacles);


        GameObject temp = First;
        esagoni.Remove(First);
        esagoni.Add(temp);

    }

    bool IsOnPc() {
        return false;// SystemInfo.deviceType == DeviceType.Desktop;
    }

    bool IsOnMobile() {
        return true;//SystemInfo.deviceType == DeviceType.Handheld;
    }


}
