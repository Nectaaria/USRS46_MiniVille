using UnityEngine;
using System;
using System.Collections.Generic;

public class AI : Player
{
    public string Name;
    public AI(string name) : base(name)
    {
        Name = name;
    }
}
