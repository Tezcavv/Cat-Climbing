using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;

    private void Update()
    {
        // Ruota la moneta lungo l'asse Y a una velocità costante
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

}
