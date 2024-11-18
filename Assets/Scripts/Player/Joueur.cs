using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Miniville
{
    public class Joueur
    {
        Dictionary<Cards, int> DeckRouge = new Dictionary<Cards, int>();
        Dictionary<Cards, int> DeckBleu = new Dictionary<Cards, int>();
        Dictionary<Cards, int> DeckVert = new Dictionary<Cards, int>();

        public string nom;
        public int coins = 3;

        public Joueur(string Nom)
        {
            this.nom = Nom;
            //var ble = new Cards()
            //{
            //    info = new CardsInfo() { Id = 1, Color = "Bleue", Cost = 1, Name = "Champ de blé", Effect = "Gain 1 coin every turn", Dice = 1, Gain = 1 }
            //};
            //var boulangerie = new Cards()
            //{
            //    info = new CardsInfo() { Id = 3, Color = "VerteThune", Cost = 3, Name = "Boulangerie", Effect = "Gagne 1 pièce de la banque pendant votre tour", Dice = 2, Gain = 1 }
            //};
            //DeckBleu.Add(ble, 1);
            //DeckVerte.Add(boulangerie, 1);
        }
        public void ActivateEffect(int resultDe)
        {
            //bleue et verte
            int buildingCount = 0;
            foreach (var item in DeckVert)//Pour toutes les cartes vertes
            {
                if (item.Key.info.Dice != resultDe)//Si le resultat du de est different de celui necessaire a l'activation de la carte
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
                        coins += item.Key.info.Gain * buildingCount;//Donne l'argent au joueur
                    }
                    else
                    {
                        coins += item.Key.info.Gain;//Donne l'argent au joueur
                    }
                }
            }
            foreach (var item in DeckBleu)//Pour toutes les cartes bleues
            {
                if (item.Key.info.Dice != resultDe)//Si le resultat du de est different de celui necessaire
                    continue;
                for (int i = 0; i < item.Value; i++)
                {
                    coins += item.Key.info.Gain;//Donne l'argent au joueur
                }
            }
        }
        public void PassiveEffect(Joueur joueur, int resultDe)
        {
            int totalcoinslost = 0;
            int totalcoinsgained = 0;

            foreach (var item in DeckRouge)//Pour toutes les cartes rouges
            {
                if (item.Key.info.Dice != resultDe)
                    continue;
                for (int i = 0; i < item.Value; i++)
                {
                    int pieceAPrendre = Math.Min(item.Key.info.Gain, joueur.coins);
                    totalcoinslost += pieceAPrendre;//Les pieces que le joueur adverse va perdre
                    joueur.coins -= pieceAPrendre;//Enlever les pieces
                    coins += pieceAPrendre;//Ajouter au joueur actuel les pieces
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

        public string ShowCards()//Permet d'afficher les cartes
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

        public void AddCard(Cards card)//Ajoute la carte dans le deck
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
            if (deck.ContainsKey(card))//Si le deck a deja la carte, ajouter 1 au compteur de carte
            {
                deck[card]++;
            }
            else
            {
                deck.Add(card, 1);
            }
        }

        public Dictionary<Cards, int> GetPlayerCards()//Recupere toutes les cartes du joueur
        {
            Dictionary<Cards, int> allPlayerCards = new Dictionary<Cards, int>(DeckBleu);

            foreach (var card in DeckRouge)
            {
                allPlayerCards.Add(card.Key, card.Value);//Ajoute chaques cartes au dictionnaire
            }

            foreach (var card in DeckVert)
            {
                allPlayerCards.Add(card.Key, card.Value);//Ajoute chaques cartes au dictionnaire
            }

            return allPlayerCards;
        }

    }
}