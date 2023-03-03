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

    private float traveledDistance = 0;
    private float frameDistance;
    private float previousZ;
    private string initialText;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        initialText= text.text;
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();

    }

    void UpdateText() {
        if (GameManager.Instance.isGamePaused)
            return;

        frameDistance = Mathf.Abs(gameManager.First.transform.position.z - previousZ);
        traveledDistance += frameDistance/50;
        text.text = initialText + traveledDistance.ToString("#");
        previousZ = gameManager.First.transform.position.z;
    }
}
