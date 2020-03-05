using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Code by Daniel Pobijanski

public abstract class Player : MonoBehaviour
{
    public TurnController _turnController;
    public bool IsPlayer1;
    public int CurrentX { set; get; }
    public int CurrentY { set; get; }

    static public string winPlayerNickname = "Nobody";

    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool[,] PossibleMove()
    {
        return new bool[14,20];
    }
}
