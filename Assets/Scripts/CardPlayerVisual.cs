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

        protected override void Start()
        {
            foreach (var card in cards)
            {
                cardObjects.Add(card, Instantiate(cardPrefab, transform.position, transform.rotation * Quaternion.Euler(rotationOffset), transform).GetComponent<CardVisual>());
            }

            UpdateVisual();
        }

        public void UpdateVisual()
        {
            Objects = cardObjects.Values.Select(x => x.gameObject).ToList();
            AlignGameObjectsHorizontally();
            UpdateCardsVisual();
        }

        private void AlignGameObjectsHorizontally()
        {
            if (cardObjects == null || cardObjects.Count == 0)
            {
                //Debug.LogWarning("Le dictionnaire de cartes est vide ou nul.");
                return;
            }

            int cardCount = cardObjects.Count;

            Vector3 startPosition = transform.position; // Position initiale
            Quaternion orientation = transform.rotation; // Orientation du GameObject
            int count = 0;

            foreach (var obj in cardObjects)
            {
                if (obj.Value == null)
                    continue;

                // Calcul de la position cible relative à l'orientation du GameObject
                Vector3 localOffset = new Vector3(
                    (count % maxPerRow) * spacingX * (invertX ? -1 : 1), // Décalage sur l'axe X, inversé si invertX est true
                    0, // Aucun changement sur l'axe Y
                    (count / maxPerRow) * spacingZ * (invertZ ? 1 : -1)  // Décalage sur l'axe Z, direction inversée par défaut
                );

                Vector3 targetPosition = startPosition + orientation * localOffset; // Applique l'orientation au décalage

                obj.Value.transform.position = targetPosition; // Positionne le GameObject

                count++;
            }
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
