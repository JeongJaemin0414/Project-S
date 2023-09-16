using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DOTweenManager : Singleton<DOTweenManager>
{

    public void FadeIn(Image image) //페이드 인 사용
    {
        StartCoroutine(Fade(image, true));
    }

    public void FadeOut(Image image) //페이드 아웃 사용
    {
        StartCoroutine(Fade(image, false));
    }

    private IEnumerator Fade(Image image, bool isFadeIn)
    {
        if (isFadeIn)
        {
            image.gameObject.SetActive(true);
            Color color = image.color;
            color.a = 0;
            image.color = color;
            Tween tween = image.DOFade(1f, 1f);
            yield return tween.WaitForCompletion();
        }
        else
        {
            Color color = image.color;
            color.a = 1;
            image.color = color;
            Tween tween = image.DOFade(0f, 1f);
            yield return tween.WaitForCompletion();
            image.gameObject.SetActive(false);
        }
    }
}
