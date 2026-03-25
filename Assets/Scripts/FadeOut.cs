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
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        float elapsed = 0f;
        Color c = fadePanel.color;
        c.a = 0f;
        fadePanel.color = c;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration);
            fadePanel.color = c;
            yield return null;
        }

        SceneManager.LoadScene(targetScene);
    }
}