using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figther : Player
{
    public Figther(int ap, int dp, int hp, int speed)
    {
        ap = 2;
        ap = Dmg;
        dp = Ver;
        hp = Hp;
        speed = Speed;
    }

    private int _diceNumber;
    public override bool[,] PossibleMove()
    {
        _diceNumber = DiceController.Number;

        bool[,] _currentPlayer = new bool[14, 20];
        Player _enemyPlayer;

        //Left
        if (CurrentX - _diceNumber > 0)
        {
            for (int i = 1; i < _diceNumber + 1; i++)
            {
                if ((i + 1 * _diceNumber % 2) % 2 == 0)
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
        if (CurrentX + _diceNumber < 14)
        {
            for (int i = 1; i < _diceNumber + 1; i++)
            {
                if ((i + 1 * _diceNumber % 2) % 2 == 0)
                {
                    _enemyPlayer = BoardController.Instance.Players[CurrentX + i, CurrentY];
                    if (_enemyPlayer == null)
                    {
                        _currentPlayer[CurrentX + i, CurrentY] = true;
                    }
                }
            }
        }


        //Up
        if (CurrentY + _diceNumber < 20)
        {
            for (int i = 1; i < _diceNumber + 1; i++)
            {
                if ((i + 1 * _diceNumber % 2) % 2 == 0)
                {
                    _enemyPlayer = BoardController.Instance.Players[CurrentX, CurrentY + i];
                    if (_enemyPlayer == null)
                        _currentPlayer[CurrentX, CurrentY + i] = true;
                }
            }
        }

        //Down
        if (CurrentY - _diceNumber > 0)
        {
            for (int i = 1; i < _diceNumber + 1; i++)
            {
                if ((i + 1 * _diceNumber % 2) % 2 == 0)
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
