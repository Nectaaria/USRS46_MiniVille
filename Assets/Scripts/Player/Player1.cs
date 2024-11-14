using UnityEngine;
using System;
using System.Collections.Generic;

public class Player
{
    private string name;
    private int die;
    private int coins;
    private Pile deck;

    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }
    public Player(string Name)
    {
        name = Name;
        die = 1;
        coins = 0;

    }

    public void ActivateCard()
    {
        
    }

    
}
