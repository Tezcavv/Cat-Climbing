using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UINavigationButton : MonoBehaviour, ISelectHandler {

    [SerializeField] private GameObject imageToScale;
    [Range(1, 2)][SerializeField] private float scaleMultiplier = 1f;
    [SerializeField] private Sprite buttonSelectedSprite;

    private Image uiImage;
    private Sprite originalSprite;
    private Vector3 originalScale;


    private static UINavigationButton selectedNavigationButton = null;

    private void Start() {
        originalScale = imageToScale.transform.localScale;
        uiImage=GetComponent<Image>();
        originalSprite = uiImage.sprite;
    }

    public void ResetScale() {
        imageToScale.transform.localScale = originalScale;
        uiImage.sprite = originalSprite;
    }


    public void OnSelect(BaseEventData eventData) {
        imageToScale.transform.localScale*=scaleMultiplier;
        uiImage.sprite = buttonSelectedSprite;

        UpdateSelectedButton();
        
    }

    private void UpdateSelectedButton() {

        if (!selectedNavigationButton) {
            selectedNavigationButton = this;
            return;
        }
        
        if(selectedNavigationButton != this) {
            selectedNavigationButton.ResetScale();
        }

        selectedNavigationButton = this;

    }
}
