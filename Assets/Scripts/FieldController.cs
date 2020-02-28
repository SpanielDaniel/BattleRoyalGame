using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    public enum FieldState { Normal, Start, EventCard1, EventCard2}

    [SerializeField] public FieldState _fieldState = FieldState.Normal;

    void Start()
    {
        Material Material = transform.GetComponent<Renderer>().material;

        switch (_fieldState) {
            case FieldState.Normal:
                Material.color = Color.gray;
                break;
            case FieldState.Start:
                Material.color = Color.green;
                break;
            case FieldState.EventCard1:
                Material.color = Color.blue;
                break;
            case FieldState.EventCard2:
                Material.color = Color.red;
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
