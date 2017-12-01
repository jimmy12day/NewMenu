using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingLevelWithBar : MonoBehaviour
{

    public GameObject loadingScreen;
    [Tooltip("Attach a UI panel with a coponent of Image with type of Fill and set to Horizontal")]
    public Image loadingProgressBar;
    public Text loadingProgressStates;
    float progressRefined;
    AsyncOperation operation;

    public void LoadNextLevel()
    {
        LoadingLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousLevel()
    {
        LoadingLevel(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void LoadingLevel(int buildIndex)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadingLevelWithBarProgress(buildIndex));
    }


    IEnumerator LoadingLevelWithBarProgress(int buildIndex)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex);
        while (!operation.isDone)
        {
            progressRefined = ProgressReset(operation.progress, 0f, 1f);
            loadingProgressStates.text = (progressRefined * 100).ToString("0") + "%";
            loadingProgressBar.fillAmount = progressRefined;
            yield return null;

        }
    }

    float ProgressReset(float value, float min, float max)
    {
        float ProgressRestFactor = 0.9f;
        value = value / ProgressRestFactor;
        return Mathf.Clamp(value, min, max);
    }
}
