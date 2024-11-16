﻿using Microsoft.VisualBasic;
using System;
using Unity;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Minitamereville
{
    public class Game
    {
        string choix;
        string moncul;

        bool debileproof = true;
        bool canBuy = false;
        private bool isExpert;
    public void Buy(Cards card, Player player, Pile pile) // Param�tre 1: la carte � acheter, Para 2: le joueur qui ach�te, Para 3: la pile des cartes restantes du jeu
    {
        if (pile.pile.Contains(card)){
            //player.Deck.AddCard(card);
        }
    }

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

        }
        public void RunGame()
        {
            Console.WriteLine("Choix du type de la partie: rapide, standard, longue ou expert!");
            string gameTypeChoice = Console.ReadLine();

            switch (gameTypeChoice)//Choix du type de partie
            {
                case "rapide":
                    EndCoinGoal = 10;
                    break;

                case "standard":
                    EndCoinGoal = 20;
                    break;

                case "longue":
                    EndCoinGoal = 30;
                    break;

                case "expert":
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
                    Console.WriteLine("Voulez-vous acheter une carte ? (oui/non)" + String.Format(" | Pièces: {0}", joueurs[0].coins));
                    choix = Console.ReadLine().ToLower();
                    if (choix == "oui" && joueurs[0].coins >= 1)
                    {
                        Console.WriteLine("Quelle carte ?");
                        foreach (var item in pile.carteDispo)
                        {
                            Console.WriteLine(item.info.Id - 1 + " : " + item.info.Name + String.Format(" (Coût: {0})", item.info.Cost));//Affichage des cartes dispo avec les id
                        }

                        int playerChoice = int.Parse(Console.ReadLine());

                        CheckIfCanBuy(joueurs[0], playerChoice);
                    }
                    else//Sinon rien acheter
                    {
                        debileproof = false;
                        Console.WriteLine("Tour passé.");
                    }
                }
                MsgEntry(joueurs[1]);
                result = de.Lancer();
                Console.WriteLine("Dé : " + de.ToString());//Afficher le de

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

            var availableCards = pile.GestionStock(joueur.coins);//Cartes disponible a l'achat

            for (int i = 0; i < availableCards.Count; i++)//Pour toute les cartes dispo
            {
                if (availableCards.Count >= 1 && availableCards[i].info.Id - 1 == choice)//Si le joueur peut acheter la carte
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
            string texte = String.Concat("=====> Tour de ", joueur.nom, "\nSes cartes: ", joueur.ShowCards(), "\nNombre de pièces: ", joueur.coins, "\n------");
            Console.WriteLine(texte);
        }
    }
}