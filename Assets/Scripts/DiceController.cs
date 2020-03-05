using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Code by Daniel Pobijanski

public class DiceController : MonoBehaviour
{
    [SerializeField]
    private Button _diceButton;
    private int _number;

    public bool DiceActive = true;

    public static int Number { get; set; }

    public void UseDice()
    {
            _number = Random.Range(1, 6);
            Number = _number;

            _diceButton.GetComponentInChildren<Text>().text = "Dice: " + _number;
            _diceButton.interactable = false;

    }
}
