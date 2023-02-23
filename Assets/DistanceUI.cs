using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceUI : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;

    private TextMeshProUGUI text;

    [SerializeField]
    private string initialText;

    private float traveledDistance = 0;
    private float frameDistance;
    private float previousZ;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();

    }

    void UpdateText() {
        if (GameManager.isGamePaused)
            return;

        frameDistance = Mathf.Abs(gameManager.First.transform.position.z - previousZ);
        traveledDistance += frameDistance/50;
        text.text = initialText + traveledDistance.ToString("#");
        previousZ = gameManager.First.transform.position.z;
    }
}
