using UnityEngine;
using System.Collections;


public class User
{
    private string _username;

    public string Username
    {
        get
        {
            return _username;
        }
        set
        {
            _username = Username;
        }
    }

    private Inventory _inventory;

    private int _gold = 0;

    public int Gold
    {
        get { return _gold; }
        set
        {
            if ((_gold + value) < 0)
            {
                _gold = 0;
            }
            else
            {
                _gold = value;
            }
        }
    }


    public User(string aUsername)
    {
        Username = aUsername;
        _inventory = new Inventory(500);
    }
    
}
