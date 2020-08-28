using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHub : MonoBehaviour
{
    public void StartGame()
    {
        GameSceneManager.Instance.NextScene("SampleScene");
    }
}
