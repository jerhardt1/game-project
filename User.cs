using UnityEngine;
using System.Collections;

public class User
{
    private string _username;
    private Inventory _inventory;
    


    public string Username
    {
        get { return _username; }
        set { _username = Username;  }
    }


    public User(string aUsername)
    {
        Username = aUsername;
        _inventory = new Inventory(500);
    }

    private void InitializeUser()
    {

    }


    
}
