using UnityEngine;
using System.Collections.Generic;
using Miniville;
using System.Linq;

namespace Miniville
{
    public class CardPlayerVisual : GameObjectAligner
    {
        // Champs publics configurables depuis l'inspecteur dans Unity
        public Dictionary<Cards, CardVisual> cardObjects = new Dictionary<Cards, CardVisual>(); // Dictionnaire des cartes et leurs GameObjects
        public List<Cards> cards = new List<Cards>();
        public GameObject cardPrefab;

        private void Start()
        {
            foreach (var card in cards)
            {
                cardObjects.Add(card, Instantiate(cardPrefab, transform.position, GetRotation(), transform).GetComponent<CardVisual>());
            }

            UpdateVisual();
        }

        public void UpdateVisual()
        {
            Objects = cardObjects.Values.Select(x => x.gameObject).ToList();
            AlignGameObjectsHorizontally();
            UpdateCardsVisual();
        }

        private void UpdateCardsVisual()
        {
            foreach (var item in cardObjects)
            {
                //item.Value.Card = item.Key;
            }
        }
    }
}
