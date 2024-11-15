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
        string nom;
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
            foreach (var item in DeckRouge)
            {   
                if (item.Key.info.Dice != resultDe)
                    continue;
                for (int i = 0; i < item.Value; i++)
                {
                    int pieceAPrendre = Math.Min(item.Key.info.Gain, joueur.coins);
                    joueur.coins -= pieceAPrendre;
                    coins += pieceAPrendre;
                }
                
            }
            foreach(var item in DeckBleu)
            {
                if (item.Key.info.Dice != resultDe)
                    continue;
                for (int i = 0; i < item.Value; i++)
                {
                    coins += item.Key.info.Gain;
                }
            }
                //bleue et rouge
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
