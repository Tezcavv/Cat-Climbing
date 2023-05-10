using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinText : MonoBehaviour
{

    private TextMeshProUGUI myTextMeshProUGUI;
    void Awake()
    {
        myTextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Start() {

        CurrencyManager.Instance.OnCoinUpdate.AddListener(UpdateText);
        myTextMeshProUGUI.text = CurrencyManager.coins.ToString();
    }

    public void UpdateText(int coins) {
        myTextMeshProUGUI.text = coins.ToString();
    }
}
