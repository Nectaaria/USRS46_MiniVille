using UnityEngine;

public class CardClass : MonoBehaviour
{
    public struct CardsInfo
    {
        //Attributes with accessors
        public int Id { get; set; }
        public string Color { get; set; }
        public int Cost { get; set; }
        public string Name { get; set; }
        public string Effect { get; set; }
        public int Dice { get; set; }
        public int Gain { get; set; }
    }

    public class Cards
    {
        public Cards(int id, string color, int cost, string name, string effect, int dice, int gain)
        {
            CardsInfo cardsInfo = new CardsInfo();

            cardsInfo.Id = id;
            cardsInfo.Color = color;
            cardsInfo.Cost = cost;
            cardsInfo.Name = name;
            cardsInfo.Effect = effect;
            cardsInfo.Dice = dice;
            cardsInfo.Gain = gain;
        }
    }
}
