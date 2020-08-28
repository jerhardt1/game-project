using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;



public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance = null;

    private Scene _currentScene;
    public event Action<float> OnProgress = delegate { };


    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
    }


    public void NextScene(string aSceneID)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LoadingScreen");
        StartCoroutine(LoadASync(aSceneID));
    }



    IEnumerator LoadASync(string aSceneID)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(aSceneID);
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / .9f);
            OnProgress(progress);
            Debug.Log(progress);
            yield return null;
        }


    }

}
