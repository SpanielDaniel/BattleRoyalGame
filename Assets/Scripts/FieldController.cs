using UnityEngine;
//Code by Vitali Kross

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
                Material.color = Color.black;
                break;
            case FieldState.EventCard1:
                Material.color = Color.white;
                break;
            case FieldState.EventCard2:
                Material.color = Color.white;
                break;
            default:
                Material.color = Color.white;
                break;
        }
    }
}
