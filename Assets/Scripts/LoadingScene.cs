using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public CanvasGroup c;
    public string nextScene;
    public GameConstants gameConstants;

    void Start()
    {
        if (gameConstants.currentLives <= 0)
        {
            ReturnToMain();
        }
        else
        {
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        if (c != null)
        {
            for (float alpha = 1f; alpha >= -0.05f; alpha -= 0.05f)
            {
                c.alpha = alpha;
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }

        if (!string.IsNullOrEmpty(nextScene))
        {
            // once done, go to next scene
            SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
        }
    }

    public void ReturnToMain()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Single);
    }
}
