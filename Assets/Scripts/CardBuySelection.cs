using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace Miniville
{
    public class CardBuySelection : MonoBehaviour
    {
        public static CardBuySelection instance;

        public Game game;

        public List<CardVisual> cardVisuals;

        bool _isActive;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);//Enlever les cartes
                return;
            }

            instance = this;
        }

        public void Init(List<Cards> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (i >= cardVisuals.Count)
                    break;

                cardVisuals[i].Card = cards[i];//Creer les cartes et leur visuel
            }
        }

        public void StartCardChoice()
        {
            _isActive = true;
            // Activate card clicks ?
        }

        public void StopCardChoice()
        {
            _isActive = false;
        }

        public void BuyCard(Cards card)
        {
            if (!_isActive)
                return;

            bool didBuyCard = game.JoueurBuyCard(card);
            Debug.Log($"{nameof(didBuyCard)}: {didBuyCard}");
            if (didBuyCard)
                StopCardChoice();
        }

        public void UpdateVisual(Dictionary<Cards, int> paquet)
        {
            foreach (var visual in cardVisuals)
            {
                visual.gameObject.SetActive(paquet.ContainsKey(visual.Card) && paquet[visual.Card] > 0);
            }
        }
    }
}
