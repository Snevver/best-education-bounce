using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 1.5f;
    public string targetScene = "MainMenu";

    public void StartFade()
    {
        if (fadePanel == null) return;

        Color c = fadePanel.color;
        c.a = 0f;
        fadePanel.color = c;
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        float elapsed = 0f;
        Color c = fadePanel.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration);
            fadePanel.color = c;
            yield return null;
        }

        c.a = 1f;
        fadePanel.color = c;
        SceneManager.LoadScene(targetScene);
    }
}