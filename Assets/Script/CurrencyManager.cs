using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }
 
    public UnityEvent OnCoinPickup = new UnityEvent();
    public UnityEvent<int> OnCoinUpdate = new UnityEvent<int>(); 

    public static int coins = 20;
    private void Awake() {
      
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void Start() {
        OnCoinPickup.AddListener(AddCoin);
    }

    public void AddCoin() {
        coins++;
        OnCoinUpdate?.Invoke(coins);
    }

}
