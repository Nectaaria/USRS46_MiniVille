using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;


namespace Miniville
{
    [Serializable]
    public class Joueur
    {
        Dictionary<Cards, int> DeckRouge = new Dictionary<Cards, int>();
        Dictionary<Cards, int> DeckBleu = new Dictionary<Cards, int>();
        Dictionary<Cards, int> DeckVert = new Dictionary<Cards, int>();
        public string nom;
        public int coins
        {
            get => _coins;
            set
            {
                _coins = value;
                if (moneyText != null)
                    moneyText.text = value.ToString();
            }
        }

        public TextMeshProUGUI moneyText;
        public CardPlayerVisual cardPlayerVisual;

        [SerializeField] private int _coins = 3;

        public void Init()
        {
            coins = _coins;
        }

        public void ActivateEffect(int resultDe)
        {
            //bleue et verte
            int buildingCount = 0;
            foreach (var item in DeckVert)
            {
                if (item.Key.info.Dice != resultDe)
                    continue;
                foreach (var targetItem in DeckBleu)
                {
                    if (targetItem.Key.info.Type == item.Key.info.TargetType)
                    {
                        buildingCount += targetItem.Value;
                    }
                }
                for (int i = 0; i < item.Value; i++)
                {
                    if (resultDe > 6)
                    {
                        coins += item.Key.info.Gain * buildingCount;
                    }
                    else
                    {
                        coins += item.Key.info.Gain;
                    }
                }
            }
            foreach (var item in DeckBleu)
            {
                if (item.Key.info.Dice != resultDe)
                    continue;
                for (int i = 0; i < item.Value; i++)
                {
                    coins += item.Key.info.Gain;
                }
            }
        }
        public void PassiveEffect(Joueur joueur, int resultDe)
        {
            int totalcoinslost = 0;
            int totalcoinsgained = 0;
            foreach (var item in DeckRouge)
            {
                Console.WriteLine(item.Key.info.Name);
                if (item.Key.info.Dice != resultDe)
                    continue;
                for (int i = 0; i < item.Value; i++)
                {
                    int pieceAPrendre = Math.Min(item.Key.info.Gain, joueur.coins);
                    totalcoinslost += pieceAPrendre;
                    joueur.coins -= pieceAPrendre;
                    coins += pieceAPrendre;
                    totalcoinsgained += pieceAPrendre;
                }

            }
            foreach (var item in DeckBleu)
            {
                if (item.Key.info.Dice != resultDe)
                    continue;
                for (int i = 0; i < item.Value; i++)
                {
                    totalcoinsgained += item.Key.info.Gain;
                    coins += item.Key.info.Gain;
                }
            }
            //bleue et rouge
            Console.WriteLine("{0} gagne {1} coins dû aux capacités passives de ses cartes.", joueur.nom, totalcoinsgained);
        }

        public string ShowCards()
        {
            string texte = "";
            foreach (var card in DeckBleu)
            {
                texte += card.Key.info.Name + " | ";
            }

            foreach (var card in DeckRouge)
            {
                texte += card.Key.info.Name + " | ";
            }
            foreach (var card in DeckVert)
            {
                texte += card.Key.info.Name + " | ";
            }

            return texte;
        }

        public void AddCard(Cards card)
        {
            Dictionary<Cards, int> deck = new Dictionary<Cards, int>();
            switch (card.info.Color)
            {
                case CardColor.Bleu:
                    deck = DeckBleu;
                    break;
                case CardColor.Rouge:
                    deck = DeckRouge;
                    break;
                case CardColor.Vert:
                    deck = DeckVert;
                    break;

            }
            if (deck.ContainsKey(card))
            {
                deck[card]++;
            }
            else
            {
                deck.Add(card, 1);
            }

            if (cardPlayerVisual != null)
                cardPlayerVisual.AddCard(card);
        }

        public Dictionary<Cards, int> GetPlayerCards()
        {
            Dictionary<Cards, int> allPlayerCards = new Dictionary<Cards, int>(DeckBleu);

            foreach (var card in DeckRouge)
            {
                allPlayerCards.Add(card.Key, card.Value);
            }

            foreach (var card in DeckVert)
            {
                allPlayerCards.Add(card.Key, card.Value);
            }

            return allPlayerCards;
        }

    }
}