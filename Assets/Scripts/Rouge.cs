using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rouge : Player
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[14, 20];
        Player c, c2;

        //Diagonal Left
        if(CurrentX != 0 && CurrentY != 7)
        {
            c = BoardManager.Instance.Players[CurrentX - 1, CurrentY + 1];
            if (c != null)
            {
                r[CurrentX - 1, CurrentY + 1] = true;
            }
        }

        //Diagonal Right
        if (CurrentX != 7 && CurrentY != 7)
        {
            c = BoardManager.Instance.Players[CurrentX + 1, CurrentY + 1];
            if (c != null)
            {
                r[CurrentX + 1, CurrentY + 1] = true;
            }
        }

        //Middle
        if(CurrentY != 7)
        {
            c = BoardManager.Instance.Players[CurrentX, CurrentY + 1];
            if (c == null)
                r[CurrentX, CurrentY + 1] = true;
        }

        return r;
    }
}
