using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingLogo : MonoBehaviour
{
    float duration = 3f;
    Vector3 originalScale;
    void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(ScaleLogo());
       
    }

    IEnumerator ScaleLogo() {
        while(true) {

            gameObject.transform.DOScale(originalScale * 1.2f, duration);

            yield return new WaitForSeconds(duration/2);

            gameObject.transform.DOScale(originalScale, duration);

            yield return new WaitForSeconds(duration/2);

        }
        
    }

}
