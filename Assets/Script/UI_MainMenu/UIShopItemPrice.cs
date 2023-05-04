using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopItemPrice : MonoBehaviour
{
    private enum CurrencySelected { Soft, Hard}

    [SerializeField] private CurrencySelected currencySelected;

    [SerializeField] private Sprite softCurrenctySprite;
    [SerializeField] private Sprite hardCurrencySprite;

    [SerializeField] private int price;

    void Start()
    {
        Image image = GetComponentInChildren<Image>();
        TextMeshProUGUI priceText = GetComponentInChildren<TextMeshProUGUI>();

        image.sprite=currencySelected == CurrencySelected.Soft ? softCurrenctySprite: hardCurrencySprite;
        priceText.text = price + "";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
