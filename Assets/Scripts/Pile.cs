using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    public class Pile
    {
        public Dictionary<Cards, int> paquet = new Dictionary<Cards, int>();
        public List<Cards> carteDispo = new List<Cards>();
        public Pile()
        {
            var ble = new Cards()
            {
                info = new CardsInfo() { Id = 1, Color = "Bleue", Cost = 1, Name = "Champ de blé", Effect = "Gain 1 coin every turn", Dice = 1, Gain = 1, Type = "Producteur" }
            };
            var cafe = new Cards()
            {
                info = new CardsInfo() { Id = 2, Color = "Rouge", Cost = 2, Name = "Café", Effect = "Gagne 1 pièce du joueur qui a lancé les dés", Dice = 3, Gain = 1, Type ="Commercial" }
            };
            var boulangerie = new Cards()
            {
                info = new CardsInfo() { Id = 3, Color = "VerteThune", Cost = 3, Name = "Boulangerie", Effect = "Gagne 1 pièce de la banque pendant votre tour", Dice = 2, Gain = 1, Type = "Bouffe" }
            };
            var fromagerie = new Cards()
            {
                info = new CardsInfo() { Id = 4, Color = "VerteBonus", Cost = 4, Name = "Fromagerie", Effect = "Recevez 3 pieces de la banque pour chaque établissement du type ferme que vous possédez", Dice = 7, Gain = 1, Type ="Usine"  }
            };


            paquet.Add(ble, 6);
            paquet.Add(cafe, 6);
            paquet.Add(boulangerie, 6);
            paquet.Add(fromagerie, 6);
        }
        public List<Cards> GestionStock(int coins)
        {
            carteDispo = paquet.Where(x => x.Value > 0).Select(x => x.Key).ToList(); //retourne les cartes qui sont encore en stock
            return carteDispo.Where(x => x.info.Cost <= coins).ToList();  
        }
        
        public void Buy(int moncul, Joueur joueur)
        {
            var carte = carteDispo[moncul];
            joueur.AddCard(carte);
            joueur.coins -= carte.info.Cost;
            paquet[carte]--;
            if(paquet[carte] <= 0)
            {
                carteDispo.Remove(carte);
            }
        }
    }
}
