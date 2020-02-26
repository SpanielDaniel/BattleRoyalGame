using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rouge : Player
{
    private int _number = DiceController.Number;


    public override bool[,] PossibleMove()
    {
        
        bool[,] r = new bool[14, 20];
        Player c;

        //Left
        if(CurrentX != 0 && CurrentY != 19)
        {
            c = BoardManager.Instance.Players[CurrentX - _number, CurrentY];
            if (c == null)
            {
                r[CurrentX - _number, CurrentY] = true;
            }
        }

        //Right
        if (CurrentX != 13 && CurrentY != 19)
        {
            c = BoardManager.Instance.Players[CurrentX +_number, CurrentY ];
            if (c == null)
            {
                r[CurrentX + _number, CurrentY] = true;
            }
        }

        //Up
        if(CurrentY != 19)
        {
            c = BoardManager.Instance.Players[CurrentX, CurrentY + _number];
            if (c == null)
                r[CurrentX, CurrentY + _number] = true;
        }

        //Down
        if (CurrentY != 0)
        {
            c = BoardManager.Instance.Players[CurrentX, CurrentY - _number];
            if (c == null)
                r[CurrentX, CurrentY - _number] = true;
        }

        return r;
    }
}
