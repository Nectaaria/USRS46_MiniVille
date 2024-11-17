using System;
using UnityEngine;

namespace Miniville
{

    [CreateAssetMenu(fileName = "New Card", menuName = "Miniville/Card")]
    public class Cards : ScriptableObject
    {
        

        public CardsInfo info;
        public Sprite Visual;
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
        public string Type;
        public string TargetType;
    }
    
}

