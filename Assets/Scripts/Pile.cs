using System.Collections.Generic;
using UnityEngine;

public class Pile
{
    //Attributes
    public List<Cards> pile = new List<Cards>();
    private bool isEmpty { get; set; } = false;

    public Pile(List<Cards> Pile)//Any pile as an argument (game pile, player pile ...)
    {
        pile = Pile;
    }

    public bool CheckCards()//Check how many cards are remaining
    {
        if (pile.Count == 0)
        {
            isEmpty = true;
        }
        return isEmpty;
    }

    public void GetEachCards()//Gets all cards
    {
        foreach (var card in pile)
        {
            //Debug.Log(card.GetName());
        }
    }

    public void AddCard(Cards card)
    {
        pile.Add(card);
    }

    public void RemoveCard(Cards card)
    {
        pile.Remove(card);
    }
}
