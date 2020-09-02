using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class UIIngame : MonoBehaviour
{
    [SerializeField]
    RectTransform _container = null;

    [SerializeField]
    private float _posY = 1100f;

    [SerializeField]
    private float _slideSpeed = 1f;

    [SerializeField]
    GameObject _pauseMenu = null;

    [SerializeField]
    GameObject _gameOverMenu = null;

    [SerializeField]
    GameObject _blocker = null;

    private void Awake()
    {
        Player.OnDeath += ShowGameOver;   
    }

    private void Start()
    {
        Vector3 containerPos = _container.transform.position;
        containerPos.y = _posY;
        _container.transform.position = containerPos;
        _pauseMenu.SetActive(false);
        _gameOverMenu.SetActive(false);
        _blocker.SetActive(false);

    }

    private void ShowGameOver()
    {
        _container.transform.DOLocalMoveY(0, _slideSpeed).SetUpdate(true);
        _gameOverMenu.SetActive(true);
        _blocker.SetActive(true);
        //PauseGame();
    }
    
    private void HideGameOver()
    {
        _container.transform.DOLocalMoveY(_posY, _slideSpeed/2);
        _gameOverMenu.SetActive(false);
        _blocker.SetActive(false);
        ResumeGame();

    }

    public void ShowPauseMenu()
    {
        _container.transform.DOLocalMoveY(0, _slideSpeed).SetUpdate(true);
        _pauseMenu.SetActive(true);
        _blocker.SetActive(true);
        PauseGame();

    }

    public void HidePause()
    {
        _container.transform.DOLocalMoveY(_posY, _slideSpeed/2);
        _pauseMenu.SetActive(false);
        _blocker.SetActive(false);
        ResumeGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void ReviveGems()
    {
        HideGameOver();
        Player.instance.Revive();

    }

    public void BackToHub()
    {
        ResumeGame();
        UserManager.Instance.ModifyGold(LevelGenerator.instance.ReceivedGold);
        GameSceneManager.Instance.NextScene("Hub");
    }

    private void OnDisable()
    {
        Player.OnDeath -= ShowGameOver;
    }



}
