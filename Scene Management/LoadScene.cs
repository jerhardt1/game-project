using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LoadScene : MonoBehaviour
{
    
    public LoadScene(string aSceneID)
    {
        SceneManager.LoadScene("LoadingScreen");
        StartCoroutine(LoadASync(aSceneID));
    }

    public void NextScene (string aSceneID)
    {
        SceneManager.LoadScene("LoadingScreen");
        StartCoroutine(LoadASync(aSceneID));
    }

    IEnumerator LoadASync(string aSceneID)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(aSceneID);

        

        while (!asyncLoad.isDone)
        {
            yield return new WaitForSeconds(5);
        }


    }
}
