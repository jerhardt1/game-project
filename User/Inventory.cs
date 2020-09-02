using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory 

{
    [SerializeField]
    private int _gold;

    public int Gold
    {
        get { return _gold; }
        set { _gold = value; }
    }

    public Inventory(int aStartingGold)
    {
        Gold = aStartingGold;
    }

    public void AddGold(int aAmount)
    {
        Gold += aAmount;
    }


}
