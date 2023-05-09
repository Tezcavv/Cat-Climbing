using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    

    //da pulire
    IController controller;

    [SerializeField]
    float mobileScreenPercentage;
    [SerializeField]
    GameObject exagonPrefab;
    [SerializeField]
    [Range(1, 40)]
    private int numEsagoni;

    List<Exagon> esagoni;
    public bool isGamePaused;
    private bool canRotate = true;
    public Exagon First => esagoni[0];
    Exagon Last => esagoni.LastOrDefault();
    [Range(0, 2)]
    public float rotationCooldown;
    public CharacterManager player;
    public float maxHeightForMovement;
    public float sidewaysJumpDuration;


    //game difficutly
    [SerializeField]private float gameSpeed = 1;
    public float GameSpeed => gameSpeed;
    private float timer = 0;
    [SerializeField] private float difficultyIncreaseInterval = 30f;


    public static GameManager Instance { get; private set; }

    [SerializeField]
    private UIPauseMenu pauseMenu;


    private void Awake() {
        Instance= this;
    }

    private void Start() {
        

        isGamePaused= false;
        //TOFIX
        Time.timeScale= 1.0f;

        controller = ControllerFactory.Instance;

        SpawnTerrain();

    }





    private void Update() {
        //update GameManager
        if (First.transform.position.z <= -600) {
            ResetTerrain();
        }
        HandleGameSpeed();
        HandleRotation();

        controller.ManagePause();
        controller.ManageExit();
    }

    void HandleGameSpeed() {
        timer += Time.deltaTime;
        if(timer >= difficultyIncreaseInterval) {
            timer = 0;
            gameSpeed += 0.1f;
        }
    }

    void HandleRotation() {

        if (!canRotate)
            return;

        if (!controller.InputIsValid())
            return;

        Direction dir = controller.GetDirection();

        if (dir != Direction.Left && dir != Direction.Right)
            return;

        canRotate = false;
        esagoni.ForEach(exagon => exagon.Rotate(dir));

        if (dir == Direction.Left) {
            player.animator.Play("DodgeSinistra_FULL");
        } else if (dir == Direction.Right) {
            player.animator.Play("DodgeDestra_FULL");
        }

        Invoke(nameof(Cooldown), rotationCooldown);
        

    }




    public void SpawnTerrain() {
        esagoni = new List<Exagon>();

        GameObject temp;
        for (int i = 0; i < numEsagoni; i++) {
            temp = Instantiate(exagonPrefab, new Vector3(0, 0, i * 500), Quaternion.identity);
            esagoni.Add(temp.GetComponent<Exagon>());
        }
    }

    public void ResetTerrain() {
        First.transform.position = Last.transform.position + new Vector3(0, 0, 500);

        Exagon exagon;

        exagon = First.GetComponent<Exagon>();
        exagon.DeactivateAlllevels();
        exagon.PickALevel();


        Exagon temp = First;
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

    void Cooldown() {
        canRotate = true;
    }

}
