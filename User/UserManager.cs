using UnityEngine;
using System.Collections;
using System;


public class UserManager : MonoBehaviour
{
    public static UserManager Instance = null;

    private User _currentUser = null;

    public User CurrentUser
    {
        get
        {
            return _currentUser;
        }
        set
        {
            _currentUser = value;
        }

    }

    public event Action<int> OnGoldChange = delegate { };


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

        DontDestroyOnLoad(gameObject);



        CreateNewUser();
    }

    private void Start()
    {
        GameSceneManager.Instance.OnNewSceneLoaded += UpdateUserGold;
    }

    public void CreateNewUser()
    {
        _currentUser = new User("Player 1");
    }

    public void ModifyGold(int aValue)
    {

        _currentUser.Gold += aValue;

    }

    private void UpdateUserGold()
    {
        OnGoldChange(_currentUser.Gold);
    }





}
