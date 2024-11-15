using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Minitamereville
{
    public class Game
    {
        string choix;
        string moncul;
        bool debileproof = true;
        private De de = new De("6");
        public int result;
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
            while (!End())
            {
                result = de.Lancer(); //joueur 1 lance dé
                joueurs[1].PassiveEffect(joueurs[0], result); //joueur 2 active effet passif
                joueurs[0].ActivateEffect(result); //joueur 1 active effet actif
                debileproof = true;
                pile.GestionStock();
                pile.GestionPortMonnaie(joueurs[0].coins);
                while (debileproof)
                {
                    Console.WriteLine("Voulez-vous acheter une carte ? (Oui/Non)");
                    choix = Console.ReadLine();
                    if (choix == "Oui" && joueurs[0].coins >= 1)
                    {
                        Console.WriteLine("Quelle carte ?");
                        foreach (var item in pile.carteDispo)
                        {
                            Console.WriteLine(item.info.Name);
                        }
                        moncul = Console.ReadLine();
                        pile.Buy(Int32.Parse(moncul), joueurs[0]);
                        debileproof = false;
                    }
                    else
                    {
                        debileproof = false;
                        Console.WriteLine("Vous n'avez pas suffisamment de pièces.");
                    }
                }
                result = de.Lancer();
                joueurs[0].PassiveEffect(joueurs[1], result); //joueur 1 active effet passif
                joueurs[1].ActivateEffect(result); //joueur 2 active effet actif
                pile.GestionStock();
                pile.GestionPortMonnaie(joueurs[1].coins);
                if (joueurs[1].coins >= 1)
                {
                    pile.Buy(random.Next(pile.carteDispo.Count), joueurs[0]);
                }
            }
            Console.WriteLine("Fin de la partie");
        }
        public bool End()
        {
            return joueurs[0].coins >= 20 || joueurs[1].coins >= 20;
        }
    }
}
