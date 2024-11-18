using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System.Collections;
using TMPro;


namespace Miniville
{

    public class Game : MonoBehaviour
    {
        public List<Cards> StartingCards;

        public int gameTypeChoice=1;
        string choix;

        bool debileproof = true;
        bool canBuy = false;
        public bool isExpert;

        private int EndCoinGoal = 20;
        public int result;

        private Dice de = new Dice(6);

        public GameObject diceChoice;
        public GameObject buyChoice;
        public TMP_Text outputText;

        public Pile pile;
        public SmoothTransition transition;
        public List<Joueur> joueurs;

        Random random = new Random();

        bool canContinueAction;
        int diceCount;

        private void Start()
        {
            //joueurs = new List<Joueur>();
            //joueurs.Add(new Joueur("Audrey"));
            //joueurs.Add(new AI("Pascal"));

            foreach (var joueur in joueurs)
            {
                joueur.Init();
                foreach (var card in StartingCards)
                {
                    joueur.AddCard(card);//Ajout des cartes de base au joueur
                }
            }
        }

        public void StartGame() => StartCoroutine(RunGame_Coroutine());

        public void RunGame()//Pour le jeu console
        {
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

                

                default:
                    EndCoinGoal = 20;
                    break;
            }

            while (!End())
            {
                MsgEntry(joueurs[0]);

                result = de.Lancer(); //joueur 1 lance dé
                outputText.text = "Dé : " + result + "\n";
                joueurs[1].PassiveEffect(joueurs[0], result); //joueur 2 active effet passif
                joueurs[0].ActivateEffect(result); //joueur 1 active effet actif
                debileproof = true;
                pile.GestionStock(joueurs[0].coins);

                while (debileproof)
                {
                    outputText.text = "Voulez-vous acheter une carte ? (oui/non)" +
                                      String.Format(" | Pièces: {0}", joueurs[0].coins);
                    choix = Console.ReadLine().ToLower();
                    if (choix == "oui" && joueurs[0].coins >= 1)
                    {
                        outputText.text = "Quelle carte ?";
                        foreach (var item in pile.carteDispo)
                        {
                            outputText.text = item.info.Id - 1 + " : " + item.info.Name +
                                              String.Format(" (Coût: {0})",
                                                  item.info.Cost); //Affichage des cartes dispo avec les id
                        }

                        int playerChoice = int.Parse(Console.ReadLine());

                        CheckIfCanBuy(joueurs[0], playerChoice);
                    }
                    else //Sinon rien acheter
                    {
                        debileproof = false;
                        outputText.text = "Tour passé.";
                    }
                }

                MsgEntry(joueurs[1]);
                result = de.Lancer();
                outputText.text = "Dé : " + de.ToString(); //Afficher le de

                joueurs[0].PassiveEffect(joueurs[1], result); //joueur 1 active effet passif
                joueurs[1].ActivateEffect(result); //joueur 2 active effet actif

                pile.GestionStock(joueurs[1].coins);

                if (joueurs[1].coins >= 1)//Si le joueur a des sous
                {

                    int randomChoice = random.Next(pile.carteDispo.Count);//Choisir une carte aleatoire
                    CheckIfCanBuy(joueurs[1], randomChoice);
                }
            }
            outputText.text = "Fin de la partie";
        }

        public IEnumerator RunGame_Coroutine()
        {
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

                default:
                    EndCoinGoal = 20;
                    break;
            }

            while (!End())
            {
                //MsgEntry(joueurs[0]);
                // demander le nombre de dés
                diceChoice.SetActive(true);
                yield return new WaitUntil(() => canContinueAction == true);
                canContinueAction = false;

                result = de.Lancer(diceCount); //joueur 1 lance dé
                outputText.text = $"Lancer de dé du joueur: {result}";
                // Feedback sur les effets passifs et actifs
                // Pièce up pour chaque carte activée
                joueurs[1].PassiveEffect(joueurs[0], result); //joueur 2 active effet passif
                joueurs[0].ActivateEffect(result); //joueur 1 active effet actif

                yield return new WaitForSeconds(1);

                if (pile.GestionStock(joueurs[0].coins).Count > 0)
                {
                    buyChoice.SetActive(true);
                    yield return new WaitUntil(() => canContinueAction == true);
                    canContinueAction = false;

                }

                yield return new WaitForSeconds(1);

                //MsgEntry(joueurs[1]);
                result = de.Lancer();
                outputText.text = $"Lancer de dé de l'ordinateur: {result}";
                // Feedback sur les effets passifs et actifs
                joueurs[0].PassiveEffect(joueurs[1], result); //joueur 1 active effet passif
                joueurs[1].ActivateEffect(result); //joueur 2 active effet actif

                yield return new WaitForSeconds(1);

                pile.GestionStock(joueurs[1].coins);

                if (joueurs[1].coins >= 1)//Si le joueur a des sous
                {

                    int randomChoice = random.Next(pile.carteDispo.Count);//Choisir une carte aleatoire
                    CheckIfCanBuy(joueurs[1], randomChoice);
                }

                yield return new WaitForSeconds(1);
            }
            Debug.Log("Fini");
            yield return null;
        }

        private void CheckIfCanBuy(Joueur joueur, int choice)
        {

            var availableCards = pile.GestionStock(joueur.coins);//Cartes disponible a l'achat

            for (int i = 0; i < availableCards.Count; i++) //Pour toute les cartes dispo
            {
                if (availableCards.Count >= 1 &&
                    i == choice) //Si le joueur peut acheter la carte
                {
                    canBuy = true;
                    break;
                }
            }

            if (canBuy)//Si le joueur peut acheter, acheter la carte
            {
                pile.Buy(choice, joueur);
            }

            canBuy = false;

            debileproof = false;
        }

        public bool JoueurBuyCard(Cards card)
        {
            var availableCards = pile.GestionStock(joueurs[0].coins);

            if (!availableCards.Contains(card))//Si il n'y a pas de cartes disponibles
                return false;

            Debug.Log($"Buy {card}");
            pile.Buy(card, joueurs[0]);
            ContinueAction();

            // Reset la position de la camera position
            transition.SetCameraPosition(0);
            return true;
        }

        public bool End()
        {
            bool expertGoalReached = false;
            bool allValuesValid = true;

            Dictionary<Cards, int> cardAmount = new Dictionary<Cards, int>();//Dict pour stocker le nb de cartes que le joueur à

            if (isExpert)
            {
                foreach (var key in pile.cardsToInstantiate)
                {
                    if (joueurs[0].GetPlayerCards().ContainsKey(key))
                    {
                        if (cardAmount.ContainsKey(key))//Si la carte existe deja
                        {
                            cardAmount[key]++;//Incrementer
                        }
                        else
                        {
                            cardAmount[key] = 1;//Initialiser le compte si c'est la première fois que la carte est vue
                        }
                    }
                }
                foreach (var values in cardAmount.Values)
                {
                    if (values < 1)//Si les cartes ne sont pas toutes en 1 exemplaire on quitte la boucle
                    {
                        allValuesValid = false;
                        break;
                    }
                }
                if (allValuesValid)//Si il y a au moin 1 exemplaire de chaques cartes
                {
                    expertGoalReached = true;
                }
            }
            else//Dans les cas ou expert n'est pas selectionne mettre a true pour pouvoir gganer quand meme la partie (expertGoalReached est une condition de victoire)
            {
                expertGoalReached = true;
            }

            return (joueurs[0].coins >= EndCoinGoal) || (joueurs[1].coins >= EndCoinGoal && expertGoalReached);
        }

        public void MsgEntry(Joueur joueur)//Message general pour le jeu console
        {
            string texte = String.Concat("=====> Tour de ", joueur.nom, "\nSes cartes: ", joueur.ShowCards(),
                "\nNombre de pièces: ", joueur.coins, "\n------");
            outputText.text = texte;
        }

        public void ContinueAction() => canContinueAction = true;

        public void SetDiceCount(int count) => diceCount = count;
    }
}