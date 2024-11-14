using UnityEngine;
using System;
using System.Collections.Generic;

public class Player
{
    private string Name;
    private int Die;
    private int Coins;
    List<string> Deck = new List<string>();
    public Player(string name)
    {
        Name = name;
        Die = 1;
        Coins = 0;
    }

    public void ActivateCard()
    {
        
    }

    
}
