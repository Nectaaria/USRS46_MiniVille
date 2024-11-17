using System;
using Unity;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


namespace Miniville
{

    public class Game : MonoBehaviour
    {
        public List<Cards> StartingCards;

        public int gameTypeChoice=1;
        string choix;
        string moncul;

        bool debileproof = true;
        bool canBuy = false;
        public bool isExpert;

        private int EndCoinGoal = 20;
        public int result;

        private De de = new De("6");

        public Pile pile = new Pile();
        public List<Joueur> joueurs;

        Random random = new Random();

        public Game()
        {
            joueurs = new List<Joueur>();
            joueurs.Add(new Joueur("Audrey"));
            joueurs.Add(new AI("Pascal"));

            foreach (var joueur in joueurs)
            {
                foreach (var card in StartingCards)
                {
                    joueur.AddCard(card);
                }
            }
        }

        public void RunGame()
        {
            Console.WriteLine("Choix du type de la partie: rapide, standard, longue ou expert!");

            switch (gameTypeChoice) //Choix du type de partie
            {
                case 1:
                    EndCoinGoal = 10;
                    break;

                case 2:
                    EndCoinGoal = 20;
                    break;

                case 3:
                    EndCoinGoal = 30;
                    break;

                case 4:
                    EndCoinGoal = 20;
                    isExpert = true;
                    break;

                default:
                    EndCoinGoal = 20;
                    break;
            }

            while (!End())
            {
                MsgEntry(joueurs[0]);

                result = de.Lancer(); //joueur 1 lance dé
                Console.Write("Dé : " + result + "\n");
                joueurs[1].PassiveEffect(joueurs[0], result); //joueur 2 active effet passif
                joueurs[0].ActivateEffect(result); //joueur 1 active effet actif
                debileproof = true;
                pile.GestionStock(joueurs[0].coins);

                while (debileproof)
                {
                    Console.WriteLine("Voulez-vous acheter une carte ? (oui/non)" +
                                      String.Format(" | Pièces: {0}", joueurs[0].coins));
                    choix = Console.ReadLine().ToLower();
                    if (choix == "oui" && joueurs[0].coins >= 1)
                    {
                        Console.WriteLine("Quelle carte ?");
                        foreach (var item in pile.carteDispo)
                        {
                            Console.WriteLine(item.info.Id - 1 + " : " + item.info.Name +
                                              String.Format(" (Coût: {0})",
                                                  item.info.Cost)); //Affichage des cartes dispo avec les id
                        }

                        int playerChoice = int.Parse(Console.ReadLine());

                        CheckIfCanBuy(joueurs[0], playerChoice);
                    }
                    else //Sinon rien acheter
                    {
                        debileproof = false;
                        Console.WriteLine("Tour passé.");
                    }
                }

                MsgEntry(joueurs[1]);
                result = de.Lancer();
                Console.WriteLine("Dé : " + de.ToString()); //Afficher le de

                joueurs[0].PassiveEffect(joueurs[1], result); //joueur 1 active effet passif
                joueurs[1].ActivateEffect(result); //joueur 2 active effet actif

                pile.GestionStock(joueurs[1].coins);

                if (joueurs[1].coins >= 1)
                {

                    int randomChoice = random.Next(pile.carteDispo.Count);
                    CheckIfCanBuy(joueurs[1], randomChoice);
                }
            }

            Console.WriteLine("Fin de la partie");
        }

        private void CheckIfCanBuy(Joueur joueur, int choice)
        {

            var availableCards = pile.GestionStock(joueur.coins); //Cartes disponible a l'achat

            for (int i = 0; i < availableCards.Count; i++) //Pour toute les cartes dispo
            {
                if (availableCards.Count >= 1 &&
                    availableCards[i].info.Id - 1 == choice) //Si le joueur peut acheter la carte
                {
                    canBuy = true;
                    break;
                }
            }

            if (canBuy)
            {
                pile.Buy(choice, joueur);
            }

            canBuy = false;

            debileproof = false;
        }

        public bool End()
        {
            bool expertGoalReached = false;
            if (isExpert)
            {

            }
            else
            {
                expertGoalReached = true;
            }

            return joueurs[0].coins >= EndCoinGoal || joueurs[1].coins >= EndCoinGoal && expertGoalReached;
        }

        public void MsgEntry(Joueur joueur)
        {
            string texte = String.Concat("=====> Tour de ", joueur.nom, "\nSes cartes: ", joueur.ShowCards(),
                "\nNombre de pièces: ", joueur.coins, "\n------");
            Console.WriteLine(texte);
        }
    }
}