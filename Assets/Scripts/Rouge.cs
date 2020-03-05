using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Code by Daniel Pobijanski

public class Rouge : Player
{
    public string _name;
    private int _number;
    private int _hp;
    private int _maxHp;
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

    public void Attack(Player player)
    {
        
    }


    public override bool[,] PossibleMove()
    {
        _number = DiceController.Number;

        bool[,] _currentPlayer = new bool[14, 20];
        Player _enemyPlayer;

        //Left
        if(CurrentX-_number > 0)
        {
            for (int i = 1; i < _number + 1; i++)
            {
                if ((i + 1 * _number % 2) % 2 == 0)
                {
                    _enemyPlayer = BoardController.Instance.Players[CurrentX - i, CurrentY];
                    if (_enemyPlayer == null)
                    {
                        _currentPlayer[CurrentX - i, CurrentY] = true;
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
                    _enemyPlayer = BoardController.Instance.Players[CurrentX +i, CurrentY ];
                    if (_enemyPlayer == null)
                    {
                        _currentPlayer[CurrentX + i, CurrentY] = true;
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
                    _enemyPlayer = BoardController.Instance.Players[CurrentX, CurrentY + i];
                    if (_enemyPlayer == null)
                        _currentPlayer[CurrentX, CurrentY + i] = true;
                }
            }
        }

        //Down
        if (CurrentY - _number > 0)
        {
            for (int i = 1; i < _number + 1; i++)
            {
                if ((i + 1 * _number % 2) % 2 == 0)
                {
                    _enemyPlayer = BoardController.Instance.Players[CurrentX, CurrentY - i];
                    if (_enemyPlayer == null)
                        _currentPlayer[CurrentX, CurrentY - i] = true;
                }
            }
        }

        return _currentPlayer;
    }
}
