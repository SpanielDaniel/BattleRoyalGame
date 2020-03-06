using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Code by Daniel Pobijanski

public abstract class Player : MonoBehaviour
{
    public bool IsPlayer1;
    public int Dmg { get; set; }
    public int Ver { get; set; }
    public int Hp = 7;
    public int Speed { get; protected set; }
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
    public virtual void Attack(Player player)
    {

    }
}
