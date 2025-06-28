using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class ScreenFadeController : MonoBehaviour
{
    public Material fadeMaterial;
    private float fadeValue = 0f;
    private static readonly int FadeAmountID = Shader.PropertyToID("_FadeAmount");

    public void StartFadeOut(float duration)
    {
        StartCoroutine(FadeTo(1f, duration));
    }

    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeTo(0f, duration));
    }

    private IEnumerator FadeTo(float target, float duration)
    {
        float start = fadeValue;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeValue = Mathf.Lerp(start, target, elapsed / duration);
            fadeMaterial.SetFloat(FadeAmountID, fadeValue);
            yield return null;
        }

        fadeValue = target;
        fadeMaterial.SetFloat(FadeAmountID, fadeValue);
    }
}
