using UnityEngine;

public class EventCard : MonoBehaviour {
    public int _id { get; private set; }
    public int _ap { get; private set; }
    public int _dp { get; private set; }
    public string _effect { get; private set; }
    public string _name { get; private set; }
    public Sprite _image { get; private set; }
    public string _description { get; private set; }

    public EventCard(int id,string name, int ap, int dp, string effect, Sprite image, string description){
        _id = id;
        _name = name;
        _ap = ap;
        _dp = dp;
        _effect = effect;
        _image = image;
        _description = description;
    }
}
