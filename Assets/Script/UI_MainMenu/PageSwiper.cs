using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

//for reference:
//https://www.youtube.com/watch?v=rjFgThTjLso


public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler {

    private enum Panel { LEFT, MIDDLE, RIGHT }

    private Vector3 middlePanelPosition;
    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    private int totalPages;
    private int currentPage = 2;



    // Start is called before the first frame update
    void Start() {
        //posizione del padre
        panelLocation = transform.position;
        middlePanelPosition = transform.position;
        totalPages = 3;
    }

    //orribile ma a quanto sembra gli onclick non accetano enum...
    public void GoToHome() {
        GoToPanel(Panel.MIDDLE);
    }

    public void GoToCustomization() {
        GoToPanel(Panel.LEFT);
    }

    public void GoToShop() {
        GoToPanel(Panel.RIGHT);
    }

    private void GoToPanel(Panel panel) {


        Vector3 endPos = middlePanelPosition;

        switch (panel) {
            case Panel.LEFT: endPos += new Vector3(Screen.width, 0, 0); break;
            case Panel.MIDDLE: endPos += Vector3.zero; break;
            case Panel.RIGHT: endPos += new Vector3(-Screen.width, 0, 0); break;
        }
        

        //enum parte da 0
        currentPage = (int)panel + 1;
        StartCoroutine(SmoothMove(transform.position, endPos, easing));
        panelLocation = endPos;
    }

    public void OnDrag(PointerEventData data) {
        //differenza in drag -> sposta la posizione
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }
    public void OnEndDrag(PointerEventData data) {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        //se il draggato è sopra la treshold
        if (Mathf.Abs(percentage) >= percentThreshold) {

            Vector3 newLocation = panelLocation;
            if (percentage > 0 && currentPage < totalPages) {
                currentPage++;
                newLocation += new Vector3(-Screen.width, 0, 0);
            } else if (percentage < 0 && currentPage > 1) {
                currentPage--;
                newLocation += new Vector3(Screen.width, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        } else {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
    }
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds) {
        float t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

}