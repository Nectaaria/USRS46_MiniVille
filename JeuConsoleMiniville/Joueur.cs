using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minitamereville
{
    public class Joueur
    {
        Dictionary<Cards, int> DeckRouge = new Dictionary<Cards, int>();
        Dictionary<Cards, int> DeckBleu = new Dictionary<Cards, int>();
        Dictionary<Cards, int> DeckVerte = new Dictionary<Cards, int>();
        public string nom;
        public int coins = 3;

        public Joueur(string Nom)
        {
            this.nom = Nom;
            var ble = new Cards()
            {
                info = new CardsInfo() { Id = 1, Color = "Bleue", Cost = 1, Name = "Champ de blé", Effect = "Gain 1 coin every turn", Dice = 1, Gain = 1 }
            };
            var boulangerie = new Cards()
            {
                info = new CardsInfo() { Id = 3, Color = "VerteThune", Cost = 3, Name = "Boulangerie", Effect = "Gagne 1 pièce de la banque pendant votre tour", Dice = 2, Gain = 1 }
            };
            DeckBleu.Add(ble, 1);
            DeckVerte.Add(boulangerie, 1);
        }
        public void ActivateEffect(int resultDe)
        {
            //bleue et verte
            int buildingCount = 0;
            foreach (var item in DeckVerte)
            {
                if (item.Key.info.Dice != resultDe)
                    continue;
                foreach(var targetItem in DeckBleu)
                {
                    if(targetItem.Key.info.Type == item.Key.info.targetType)
                        {
                            buildingCount+= targetItem.Value;
                        }
                }
                for (int i = 0; i < item.Value; i++)
                {
                    if (resultDe > 6)
                    {
                        coins += item.Key.info.Gain*buildingCount;
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
            List<string> cartesList = new List<string>();
            foreach (var item in DeckRouge)
            {
                if (item.Key.info.Dice != resultDe)
                    continue;
                for (int i = 0; i < item.Value; i++)
                {
                    int pieceAPrendre = Math.Min(item.Key.info.Gain, joueur.coins);
                    totalcoinslost += pieceAPrendre;
                    joueur.coins -= pieceAPrendre;
                    coins += pieceAPrendre;
                    totalcoinsgained += pieceAPrendre;
                    cartesList.Add(item.Key.info.Name);
                }
                
            }
            foreach(var item in DeckBleu)
            {
                if (item.Key.info.Dice != resultDe)
                    continue;
                for (int i = 0; i < item.Value; i++)
                {
                    totalcoinsgained += item.Key.info.Gain;
                    coins += item.Key.info.Gain;
                    cartesList.Add(item.Key.info.Name);
                }
            }
            //bleue et rouge
            string cartes = string.Join(", ", cartesList);
            Console.WriteLine("{0} gagne {1} coins dû aux capacités passives de ses cartes: {2}", joueur.nom, totalcoinsgained, cartes == "" ? "Aucune" : cartes);
        }

        public string ShowCards()
        {
            List<string> texte = new List<string>();
            foreach (var card in DeckBleu)
            {
                texte.Add(card.Key.info.Name);
            }

            foreach (var card in DeckRouge)
            {
                texte.Add(card.Key.info.Name);
            }
            foreach (var card in DeckVerte)
            {
                texte.Add(card.Key.info.Name);
            }

            string cartes = string.Join(" | ", texte);
            return cartes;
        }

        public void AddCard(Cards card)
        {
            Dictionary<Cards, int> deck = new Dictionary<Cards, int>();
            switch (card.info.Color)
            {
                case "Bleue":
                    deck=DeckBleu;
                    break;
                case "Rouge": 
                    deck = DeckRouge;
                    break;
                case "Verte":
                    deck = DeckVerte;
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
        }

        

    }
}
