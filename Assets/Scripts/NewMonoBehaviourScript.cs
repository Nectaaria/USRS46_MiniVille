using UnityEngine;
using System.Collections.Generic;
using System;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Pile pile;
    private Cards champBle;
    private Cards ferme;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        List<Cards> gameList = new List<Cards>();

        for (int i = 0; i < 6; i++)//For each copy of the cards (6 cards of each)
        {
            champBle = new Cards(1, "Blue", 1, "Champ de blé", "GiveMoney", 0, 1);
            ferme = new Cards(1, "Blue", 2, "Ferme", "GiveMoney", 0, 1);

            gameList.Add(champBle);
            gameList.Add(ferme);
        }

        pile = new Pile(gameList);

        if (!pile.CheckCards())
        {
            pile.GetEachCards();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
