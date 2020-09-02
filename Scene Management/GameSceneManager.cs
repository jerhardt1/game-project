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
    public event Action OnNewSceneLoaded = delegate { };


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);

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
            //Debug.Log(progress);
            yield return null;
        }

        OnNewSceneLoaded();





    }

}
