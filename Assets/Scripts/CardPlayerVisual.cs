using UnityEngine;
using System.Collections.Generic;
using Miniville;
using System.Linq;

namespace Miniville
{
    public class CardPlayerVisual : GameObjectAligner
    {
        public Dictionary<Cards, CardVisual> cardObjects = new Dictionary<Cards, CardVisual>();
        public GameObject cardPrefab;

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
                item.Value.Card = item.Key;
            }
        }

        public void AddCard(Cards card)
        {
            if (cardObjects.ContainsKey(card))
                return;

            cardObjects.Add(card, Instantiate(cardPrefab, transform.position, GetRotation(), transform).GetComponent<CardVisual>());
            UpdateVisual();
        }
    }
}
