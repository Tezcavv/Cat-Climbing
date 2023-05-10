using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExagonLevel : MonoBehaviour
{
    [SerializeField] private GameObject coinFolderGameObject;
    private List<Coin> coins;

    private void Awake() {
        coins = coinFolderGameObject.GetComponentsInChildren<Coin>().ToList();
    }


    public void ResetCoins() {
        coins.ForEach(coin => coin.gameObject.SetActive(true));
    }


}
