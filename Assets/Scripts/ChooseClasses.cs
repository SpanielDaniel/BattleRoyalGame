using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Code by Daniel Pobijanski

public class ChooseClasses : MonoBehaviour
{
    private bool _firstPick = true;
    private bool _allPicked = false;

    public enum _Classes { Rouge, Fighter, Tank };
    public static _Classes Classes1;
    public static _Classes Classes2;
    private void Update()
    {
        if (_allPicked)
        {
            SceneManager.LoadScene("Game");
        }
    }
    public void ChooseTank()
    {
        if (_firstPick)
        {
            Classes1 = _Classes.Tank;
            _firstPick = false;
        }
        else
        {
            Classes2 = _Classes.Tank;
            _allPicked = true;
        }
    }

    public void ChooseFighter()
    {
        if (_firstPick)
        {
            Classes1 = _Classes.Fighter;
            _firstPick = false;
        }
        else
        {
            Classes2 = _Classes.Fighter;
            _allPicked = true;
        }
    }

    public void ChooseRouge()
    {
        if (_firstPick)
        {
            Classes1 = _Classes.Rouge;
            _firstPick = false;
        }
        else
        {
            Classes2 = _Classes.Rouge;
            _allPicked = true;
        }
    }
}
