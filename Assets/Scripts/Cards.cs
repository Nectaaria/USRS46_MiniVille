using UnityEngine;

public class Cards
{
    private CardsInfo cardsInfo;
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

    public Cards(int id, string color, int cost, string name, string effect, int dice, int gain)
    {
        cardsInfo = new CardsInfo();

        cardsInfo.Id = id;
        cardsInfo.Color = color;
        cardsInfo.Cost = cost;
        cardsInfo.Name = name;
        cardsInfo.Effect = effect;
        cardsInfo.Dice = dice;
        cardsInfo.Gain = gain;
    }


    //Get all the information of the card
    public int GetId() { return cardsInfo.Id; }
    public string GetColor() { return cardsInfo.Color; }
    public int GetCost() { return cardsInfo.Cost; }
    public string GetName() { return cardsInfo.Name; }
    public string GetEffect() { return cardsInfo.Effect; }
    public int GetDice() { return cardsInfo.Dice; }
    public int GetGain() { return cardsInfo.Gain; }
}
