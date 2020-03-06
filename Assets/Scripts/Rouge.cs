using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Code by Daniel Pobijanski

public class Rouge : Player
{
    public string _name;
    private int _number;
    private TurnController _turnController;

    private void Start()
    {
        _turnController = GetComponent<TurnController>();
    }

    public Rouge(int hp, int ap, int dp, string Name) 
    {
        hp = Hp;
        ap = Dmg;
        dp = Ver;
    }

    public override void Attack(Player player)
    {
        if (player.Hp > 0)
        {
            if (Dmg > player.Ver)
            {
                player.Hp--;
            }
            if (Dmg <= player.Ver)
            {
                Debug.Log("Blocked");
            }
        }
    }


    public override bool[,] PossibleMove()
    {
        _number = DiceController.Number;
        _number += 2;
        

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
