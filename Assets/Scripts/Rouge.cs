using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rouge : Player
{
    public string _name;
    private int _number;
    private int _hp;
    private int _maxHp=7;
    private int _ap=4;
    private int _dp=3;

    public Rouge(int MaxHP,int Ap,int Dp, string Name) 
    {
        _maxHp = MaxHP;
        _hp = MaxHP;
        _ap = Ap;
        _dp = Dp;
        _name = Name;
    }

    public void Attack(Rouge player)
    {
        player._hp--;
    }

    public override bool[,] PossibleMove()
    {
        _number = DiceController.Number;

        bool[,] r = new bool[14, 20];
        Player c;

        //Left
        if(CurrentX-_number > 0)
        {
            for (int i = 1; i < _number + 1; i++)
            {
                if ((i + 1 * _number % 2) % 2 == 0)
                {
                    c = BoardController.Instance.Players[CurrentX - i, CurrentY];
                    if (c == null)
                    {
                        r[CurrentX - i, CurrentY] = true;
                    }
                }
            }
        }

        //Right
        if (CurrentX +_number < 14)
        {
            for (int i = 1; i < _number + 1; i++)
            {
                if ((i + 1 * _number % 2) % 2 == 0)
                {
                    c = BoardController.Instance.Players[CurrentX +i, CurrentY ];
                    if (c == null)
                    {
                        r[CurrentX + i, CurrentY] = true;
                    }
                }
            }
        }
        

        //Up
        if(CurrentY + _number < 20)
        {
            for (int i = 1; i < _number+1 ; i++)
            {
                if ((i+1*_number%2)%2==0)
                {
                    c = BoardController.Instance.Players[CurrentX, CurrentY + i];
                    if (c == null)
                        r[CurrentX, CurrentY + i] = true;
                }
            }
        }

        //Down
        if (CurrentY != 0)
        {
            for (int i = 1; i < _number + 1; i++)
            {
                if ((i + 1 * _number % 2) % 2 == 0)
                {
                    c = BoardController.Instance.Players[CurrentX, CurrentY - i];
                    if (c == null)
                        r[CurrentX, CurrentY - i] = true;
                }
            }
        }

        return r;
    }
}
