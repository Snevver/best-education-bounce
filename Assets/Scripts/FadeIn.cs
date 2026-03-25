using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 1f;

    void Start()
    {
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        Color c = fadePanel.color;
        c.a = 1f;
        fadePanel.color = c;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(1f - (elapsed / fadeDuration));
            fadePanel.color = c;
            yield return null;
        }
    }
}