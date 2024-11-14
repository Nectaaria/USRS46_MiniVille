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

    public void Buy(Cards card, Player player, Pile pile) // Param�tre 1: la carte � acheter, Para 2: le joueur qui ach�te, Para 3: la pile des cartes restantes du jeu
    {
        if (pile.pile.Contains(card)){
            player.Deck.Add(card);
        }
    }

    public bool CheckEnd()
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
