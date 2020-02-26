using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    [SerializeField]
    private Button _diceButton;

    public static int Number { get; private set; }

    public void UseDice()
    {
        Number = Random.Range(1, 6);

        _diceButton.GetComponentInChildren<Text>().text = "Dice: "+Number;
        _diceButton.interactable = false;
    }
}
