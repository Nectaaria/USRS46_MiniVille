using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Miniville
{
    public class Pile : MonoBehaviour
    {
        public CardBuySelection cardBuySelection;
        public List<Cards> cardsToInstantiate;
        [Min(0)] public int startPileCount = 6;
        public Dictionary<Cards, int> paquet = new Dictionary<Cards, int>();
        public List<Cards> carteDispo = new List<Cards>();

        public void Start()
        {
            foreach (var card in cardsToInstantiate)
            {
                paquet.Add(card, startPileCount);
                //Debug.Log($"Add {card} to dictionary");
            }

            cardBuySelection.Init(paquet.Select(x => x.Key).ToList());
        }

        public void Init()
        {
            cardBuySelection.UpdateVisual(paquet);
        }

        public List<Cards> GestionStock(int coins)
        {
            carteDispo = paquet.Where(x => x.Value > 0).Select(x => x.Key).ToList(); //retourne les cartes qui sont encore en stock
            return carteDispo.Where(x => x.info.Cost <= coins).ToList();
        }
        
        public void Buy(int index, Joueur joueur)//Permet d'acheter les cartes
        {
            var carte = carteDispo[index];
            Buy(carte, joueur);
        }

        public void Buy(Cards card, Joueur joueur)
        {
            Debug.Log($"{card.info.Name}");
            joueur.AddCard(card);//Ajouter la carte au deck du joueur
            joueur.coins -= card.info.Cost;//Enlever au joueur le prix de la carte
            paquet[card]--;//Retirer la carte du paquet
            if (paquet[card] <= 0)//Enlever la carte des cartes dispo si il n'en reste plus
            {
                paquet.Remove(card);
                cardBuySelection.UpdateVisual(paquet);
            }
        }
    }
}
