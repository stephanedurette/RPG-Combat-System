using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeManager : MonoBehaviour
{
    [SerializeField] private Image fadePanelImage;
    [SerializeField] private float fadeTime = 1f;

    public void FadeAround(Action a)
    {
        StartCoroutine(FadeAroundCoroutine(a));
    }

    public IEnumerator FadeAroundCoroutine(Action a)
    {
        yield return StartCoroutine(FadeCoroutine(true));
        a();
        yield return StartCoroutine(FadeCoroutine(false));
    }

    IEnumerator FadeCoroutine(bool fadingToBlack)
    {
        if (fadingToBlack)  
            fadePanelImage.raycastTarget = true;

        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            fadePanelImage.color = new Color(
                fadePanelImage.color.r, 
                fadePanelImage.color.g, 
                fadePanelImage.color.b, 
                Mathf.Lerp(0, 1, fadingToBlack ? elapsedTime / fadeTime : 1 - elapsedTime / fadeTime));
            yield return null;
        };

        if (!fadingToBlack) 
            fadePanelImage.raycastTarget = false;

        yield return 0;
    }

}
