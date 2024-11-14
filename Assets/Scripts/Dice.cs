using UnityEngine;

public class Dice
{
    public int FaceCount { get; private set; }

    public Dice(int faceCount)
    {
        FaceCount = faceCount;
    }

    public int Roll(int dieCount)
    {
        if (dieCount <= 0)
            return 0;

        int result = 0;

        for (int i = 0; i < dieCount; i++)
        {
            result += RollSingle();
        }

        return result;
    }

    private int RollSingle()
    {
        return Random.Range(1, FaceCount);
    }
}
