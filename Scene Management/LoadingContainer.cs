using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadingContainer : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    public Slider Slider { get { return _slider; } }

    private void Awake()
    {
        GameSceneManager.Instance.OnProgress += DisplayProgress;
    }

    private void DisplayProgress(float aProgress)
    {
        _slider.value = aProgress;
    }

    private void OnDestroy()
    {
        GameSceneManager.Instance.OnProgress -= DisplayProgress;
    }

}
