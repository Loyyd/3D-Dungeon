using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class FadeInFadeOut : MonoBehaviour
{
    private Image fadeEffectImage;
    private float fadeEffectAlpha;
    public bool TransitionComplete { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        fadeEffectImage = GameObject.Find("FadeEffect").GetComponent<Image>();
        fadeEffectAlpha = fadeEffectImage.color.a;
        TriggerFade(true);
    }

    public void TriggerFade(bool fadeIn, float duration = 0.4f) {
        TransitionComplete = false;
        if (fadeIn)
        {
            StartCoroutine(FadeEffectCoroutine(0, duration));
        }
        else
        {
            StartCoroutine(FadeEffectCoroutine(1f, duration));
        }
    }

    public IEnumerator FadeEffectCoroutine(float targetAlpha, float duration = 0.4f)
    {
        float startAlpha = fadeEffectAlpha;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            fadeEffectAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            fadeEffectImage.color = new Color(0, 0, 0, fadeEffectAlpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeEffectAlpha = targetAlpha;
        fadeEffectImage.color = new Color(0, 0, 0, fadeEffectAlpha);
        TransitionComplete = true;
    }

    public void FadeOutInWithCallback(Action callback, float duration = 0.4f) {
        StartCoroutine(FadeOutInWithCallbackCoroutine(callback, duration));
    }

    public IEnumerator FadeOutInWithCallbackCoroutine(Action callback, float duration = 0.6f)
    {
        yield return StartCoroutine(FadeEffectCoroutine(1f));
        callback.Invoke();
        yield return StartCoroutine(FadeEffectCoroutine(0f));    
    }
}
