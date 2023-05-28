using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider LoadingBarfill;

    public void LoadScene(int sceneID)
    {
        StartCoroutine(LoadSceneAsyn(sceneID));
    }

    IEnumerator LoadSceneAsyn(int sceneId) 
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingScreen.SetActive(true);
            LoadingBarfill.value = progressValue;
            yield return null;

            
        }
    }
}
