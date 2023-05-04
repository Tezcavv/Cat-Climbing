using System;
using System.Collections.Generic;
using UnityEngine;

public class TabOrderSorter : MonoBehaviour
{
    [Tooltip("la lista DEVE essere ordinata da sinistra a destra")]
    [SerializeField] private List<Canvas> orderedList;
    [SerializeField] private Canvas initialTab;
    [SerializeField] private int sortNumber;

    private void Start() {
        SetDefaultOrder();
        OrderTab(initialTab.gameObject);
    }

    private void SetDefaultOrder() {
        orderedList.ForEach(item => item.sortingOrder = sortNumber);
    }

    public void OrderTab(GameObject tab) {

        orderedList.ForEach(item => item.sortingOrder = sortNumber);
        int i = 1;
        Canvas selectedCanvas = tab.GetComponent<Canvas>();

        //se è a destra inverto l'ordine
        if (orderedList.IndexOf(selectedCanvas) > orderedList.Count / 2) {
            orderedList.Reverse();
            orderedList.ForEach(item => {
                item.sortingOrder = sortNumber - i;
                i++;
            });
            orderedList.Reverse();
        } else {
            orderedList.ForEach(item => {
                item.sortingOrder = sortNumber - i;
                i++;
            });
        }

        //sinistra a precedenza a destra


        selectedCanvas.sortingOrder = sortNumber;
    }
}
