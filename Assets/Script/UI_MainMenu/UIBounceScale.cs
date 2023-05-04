using DG.Tweening;
using System.Collections;
using UnityEngine;

public class UIBounceScale : MonoBehaviour
{
    [SerializeField] private float totalDuration = 3f;
    [Range(1f,4f)][SerializeField] private float maxScaleIncrease = 1.2f;
    Vector3 originalScale;
    void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(ScaleLogo());
       
    }

    IEnumerator ScaleLogo() {
        while(enabled) {

            gameObject.transform.DOScale(originalScale * maxScaleIncrease, totalDuration );

            yield return new WaitForSeconds(totalDuration /2);

            gameObject.transform.DOScale(originalScale, totalDuration );

            yield return new WaitForSeconds(totalDuration / 2);

        }
        
    }

}
