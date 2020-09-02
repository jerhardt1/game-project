using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIHub : MonoBehaviour
{
    [SerializeField]
    private Text _goldText = null;

    [SerializeField]
    private RectTransform _master = null;

    [SerializeField]
    private float _slideSpeed = 0.5f;

    [SerializeField]
    private float _parentMaxWidth = 500f;

    [SerializeField]
    private GameObject _currentMenu = null;



    public void StartGame()
    {
        GameSceneManager.Instance.NextScene("SampleScene");
    }

    private IEnumerator Start()
    {
        if (UserManager.Instance == null)
        {
            yield return null;
        }

        UserManager.Instance.OnGoldChange += UpdateGold;

    }

    private void Awake()
    {
        if (_goldText == null)
        {
            _goldText = transform.Find("GoldText").GetComponent<Text>();
        }

        if (_master == null)
        {
            _master = transform.Find("Master").GetComponent<RectTransform>();
        }

        if (_currentMenu == null)
        {
            foreach(Transform child in _master.gameObject.transform)
            {
                
                if(child.GetComponent<RectTransform>().localPosition.x == 0)
                {
                    _currentMenu = child.gameObject;
                }

            }
        }


    }



    private void CountUp()
    {
        int currentGold = int.Parse(_goldText.text);
        currentGold += 1;
        _goldText.text = currentGold.ToString();
    }

    public void Update()
    {
        //if (_goldChanged)
        //{
        //    if (int.Parse(_goldText.text) != LevelGenerator.instance.ReceivedGold)
        //    {
        //        CountUp();
        //    }
        //    else
        //    {
        //        _goldChanged = false;
        //    }
        //}
    }

    private void UpdateGold(int aValue)
    {
        _goldText.text = aValue.ToString();
    }

    private void OnDestroy()
    {
        UserManager.Instance.OnGoldChange -= UpdateGold;
    }

    public void SwipeLeft()
    {
        _master.transform.DOLocalMoveX((_parentMaxWidth * -1), _slideSpeed).SetUpdate(true);
    }

    public void SwipeRight()
    {
        _master.transform.DOLocalMoveX((_parentMaxWidth), _slideSpeed).SetUpdate(true);
    }
}
