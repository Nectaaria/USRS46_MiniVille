using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Game
{
    List<Player> Players;

    public Game(List<Player> players)
    {
        this.Players = players;
    }

    public void Buy() // J'attends les cartes + peut-�tre mettre en param�tre une carte, du coup on ajoute direct au deck du joueur, en v�rifiant d'abord la disponibilit�
    {

    }

    public bool CheckEnd() // Faut voir si il faut return le joueur plus qu'un bool, �a rendrait la t�che plus simple
    {
        foreach (Player p in Players)
        {
            if (p.Coins == 20)
            {
                return true;
            }
        }

        return false;
    }
}
