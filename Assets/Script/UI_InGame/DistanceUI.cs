using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceUI : MonoBehaviour
{


    private GameManager gameManager;

    private TextMeshProUGUI text;

    private static float traveledDistance = 0;
    private float frameDistance;
    private float currentZ;
    private float previousZ;
    private string initialText;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        text = GetComponent<TextMeshProUGUI>();
        initialText= text.text;
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();

    }

    void UpdateText() {
        if (gameManager.isGamePaused)
            return;

        currentZ = gameManager.First.transform.position.z;
        previousZ = gameManager.First.GetComponent<Movement>().lastPos.z;

        frameDistance = Mathf.Abs(gameManager.First.transform.position.z - previousZ);
        traveledDistance += frameDistance/50;
        text.text =  traveledDistance.ToString("#") + initialText;
    }
}
