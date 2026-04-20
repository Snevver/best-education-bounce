using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 1f;

    void Start() {
        if (fadePanel == null) return;
        
        Color c = fadePanel.color;
        c.a = 1f;
        fadePanel.color = c;
        StartCoroutine(FadeRoutine());
    }

    // Coroutine to handle the fade-in effect
    IEnumerator FadeRoutine() {
        float elapsed = 0f;
        Color c = fadePanel.color;

        while (elapsed < fadeDuration) {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(1f - (elapsed / fadeDuration));
            fadePanel.color = c;
            yield return null;
        }

        c.a = 0f;
        fadePanel.color = c;
    }
}