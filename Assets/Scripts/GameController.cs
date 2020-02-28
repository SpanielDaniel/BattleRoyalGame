using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Rouge Player1;
    public Rouge Player2;
    private void Start()
    {
        Player1 = new Rouge(7,4,3,"Player1");
        Player2 = new Rouge(7,4,3,"Player2");
    }


}
