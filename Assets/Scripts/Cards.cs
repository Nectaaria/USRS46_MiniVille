using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Miniville/Card")]
public class Cards : ScriptableObject
{
    [field: SerializeField] public CardsInfo cardsInfo { get; private set; }
    [field: SerializeField] public Sprite Visual { get; private set; }
}

[Serializable]
public struct CardsInfo
{
    //Attributes with accessors
    public int Id;
    public string Color;
    public int Cost;
    public string Name;
    public string Effect;
    public int Dice;
    public int Gain;
}
