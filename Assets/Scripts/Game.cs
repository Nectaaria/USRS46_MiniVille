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

    public void Buy() // J'attends les cartes + peut-être mettre en paramètre une carte, du coup on ajoute direct au deck du joueur, en vérifiant d'abord la disponibilité
    {

    }

    public bool CheckEnd() // Faut voir si il faut return le joueur plus qu'un bool, ça rendrait la tâche plus simple
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
