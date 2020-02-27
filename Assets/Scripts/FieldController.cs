using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    public enum FieldState { Normal, Start, Wall, EventCard1, EventCard2}

    [SerializeField] private FieldState _fieldState = FieldState.Normal;

    void Start()
    {
        Material Material = transform.GetComponent<Renderer>().material;

        switch (_fieldState) {
            case FieldState.Normal:
                Material.color = Color.white;
                break;
            case FieldState.Start:
                Material.color = Color.gray;
                break;
            case FieldState.Wall:
                Material.color = Color.black;
                break;
            case FieldState.EventCard1:
                Material.color = Color.red;
                break;
            case FieldState.EventCard2:
                Material.color = Color.green;
                break;
            default:
                Material.color = Color.white;
                break;
        }
    }

    void Update()
    {
        
    }
}
