using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;


    private void Start() {
        
    }

    private void Update()
    {
        // Ruota la moneta lungo l'asse Y a una velocità costante
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            CurrencyManager.Instance.OnCoinPickup?.Invoke();
            gameObject.SetActive(false);
        }

    }


}
