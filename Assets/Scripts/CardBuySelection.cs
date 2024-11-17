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

        public List<PileVisual> pileVisuals;

        bool _isActive;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        public void Init(List<Cards> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (i >= pileVisuals.Count)
                    break;

                pileVisuals[i].Init(cards[i]);
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

            if (game.JoueurBuyCard(card))
                StopCardChoice();
        }
    }
}
