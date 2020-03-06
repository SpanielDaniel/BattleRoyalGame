using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventCardsController : MonoBehaviour
{
    [SerializeField] private GameObject _eventCards;
    [SerializeField] private GameObject _closeButton;
    [SerializeField] private GameObject _openButton;
    [SerializeField] private Sprite _shield;
    [SerializeField] private Sprite _storm;
    [SerializeField] private Sprite _sword;
    [SerializeField] private GameObject _eventCardPrefab;

    private List<EventCard> eventCards = new List<EventCard>();
    private List<EventCard> player1Cards = new List<EventCard>();
    private List<EventCard> player2Cards = new List<EventCard>();

    private Text _eventCardsText;
    private int _currentTurn = 1;

    private void Start()
    {
        _eventCards.SetActive(false);
        _closeButton.SetActive(false);
        _openButton.SetActive(true);

        _eventCardsText = _openButton.transform.Find("Text").GetComponent<Text>();

        eventCards.Add(new EventCard(1, "ATK +", 1, 0, "", _sword, "Spiele diese Karte ergänzend zu deinen Würfelaugenzahl im Kampf gegen einen Gegenspieler"));
        eventCards.Add(new EventCard(2, "ATK +", 1, 0, "", _sword, "Spiele diese Karte ergänzend zu deinen Würfelaugenzahl im Kampf gegen einen Gegenspieler"));
        eventCards.Add(new EventCard(3, "ATK +", 2, 0, "", _sword, "Spiele diese Karte ergänzend zu deinen Würfelaugenzahl im Kampf gegen einen Gegenspieler"));
        eventCards.Add(new EventCard(4, "ATK +", 2, 0, "", _sword, "Spiele diese Karte ergänzend zu deinen Würfelaugenzahl im Kampf gegen einen Gegenspieler"));
        eventCards.Add(new EventCard(5, "ATK +", 3, 0, "", _sword, "Spiele diese Karte ergänzend zu deinen Würfelaugenzahl im Kampf gegen einen Gegenspieler"));
        eventCards.Add(new EventCard(6, "ATK +", 3, 0, "", _sword, "Spiele diese Karte ergänzend zu deinen Würfelaugenzahl im Kampf gegen einen Gegenspieler"));
        eventCards.Add(new EventCard(7, "ATK +", 4, 0, "", _sword, "Spiele diese Karte ergänzend zu deinen Würfelaugenzahl im Kampf gegen einen Gegenspieler"));
        eventCards.Add(new EventCard(8, "DEF +", 0, 1, "", _shield, "Spiele diese Karte ergänzend zu deinen Würfelaugenzahl im Kampf gegen einen Gegenspieler"));
        eventCards.Add(new EventCard(9, "DEF +", 0, 1, "", _shield, "Spiele diese Karte ergänzend zu deinen Würfelaugenzahl im Kampf gegen einen Gegenspieler"));
        eventCards.Add(new EventCard(10, "DEF +", 0, 2, "", _shield, "Spiele diese Karte ergänzend zu deinen Würfelaugenzahl im Kampf gegen einen Gegenspieler"));
        eventCards.Add(new EventCard(11, "DEF +", 0, 2, "", _shield, "Spiele diese Karte ergänzend zu deinen Würfelaugenzahl im Kampf gegen einen Gegenspieler"));
        eventCards.Add(new EventCard(12, "DEF +", 0, 3, "", _shield, "Spiele diese Karte ergänzend zu deinen Würfelaugenzahl im Kampf gegen einen Gegenspieler"));
        eventCards.Add(new EventCard(13, "DEF +", 0, 3, "", _shield, "Spiele diese Karte ergänzend zu deinen Würfelaugenzahl im Kampf gegen einen Gegenspieler"));
        eventCards.Add(new EventCard(14, "DEF +", 0, 4, "", _shield, "Spiele diese Karte ergänzend zu deinen Würfelaugenzahl im Kampf gegen einen Gegenspieler"));
        eventCards.Add(new EventCard(15, "VADIMS  ZORN", 0, 0, "vadims_zorn", _storm, "Der Gegner verliert einen Lebenspunkt egal ob du den Kampf gewinnst oder nicht"));
    }

    private void Update() {
        if (TurnController.GetTurn() % 2 == 1) {
            _eventCardsText.text = "Cards: " + player1Cards.Count;
        }
        else {
            _eventCardsText.text = "Cards: " + player2Cards.Count;
        }
        if (_currentTurn != TurnController.GetTurn()) {
            CloseButton();
        }
    }

    public void CloseButton() {
        _eventCards.SetActive(false);
        _closeButton.SetActive(false);
        _openButton.SetActive(true);

        foreach (Transform child in _eventCards.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void OpenButton() {
        _eventCards.SetActive(true);
        _closeButton.SetActive(true);
        _openButton.SetActive(false);

        int startPosition = 120;
        _currentTurn = TurnController.GetTurn();
        if (TurnController.GetTurn() % 2 == 1) {
            int i = 0;
            foreach (var player1Card in player1Cards) {
                string heading = "";
                if (player1Card._ap != 0) {
                    heading = player1Card._name + player1Card._ap;
                }
               else if (player1Card._dp != 0) {
                    heading = player1Card._name + player1Card._dp;
               }
               else {
                    heading = player1Card._name;
               }
                var newEventCard = GameObject.Instantiate(_eventCardPrefab, new Vector2(startPosition + i*240,150), Quaternion.identity);
                newEventCard.transform.parent = GameObject.Find("EventCards").transform;
                newEventCard.transform.Find("Image").GetComponent<Image>().sprite = player1Card._image;
                newEventCard.transform.Find("Heading").GetComponent<Text>().text = heading;
                newEventCard.transform.Find("Text").GetComponent<Text>().text = player1Card._description;
                i++;
            }
        }
        else {
            int i = 0;
            foreach (var player2Card in player2Cards) {
                string heading = "";
                if (player2Card._ap != 0) {
                    heading = player2Card._name + player2Card._ap;
                }
                else if (player2Card._dp != 0) {
                    heading = player2Card._name + player2Card._dp;
                }
                else {
                    heading = player2Card._name;
                }
                var newEventCard = GameObject.Instantiate(_eventCardPrefab, new Vector2(startPosition + i * 240, 150), Quaternion.identity);
                newEventCard.transform.parent = GameObject.Find("EventCards").transform;
                newEventCard.transform.Find("Image").GetComponent<Image>().sprite = player2Card._image;
                newEventCard.transform.Find("Heading").GetComponent<Text>().text = heading;
                newEventCard.transform.Find("Text").GetComponent<Text>().text = player2Card._description;
                i++;
            }
        }
        
        
    }
    public void UseCard() {
        Debug.Log(GameObject.Find("Heading"));
    }
    public void GetEventCard() {
        int randomCard = Random.Range(0, eventCards.Count);
        EventCard eventCard = eventCards[randomCard];
        
        if (TurnController.GetTurn() % 2 == 1) {
            player1Cards.Add(eventCard);
            eventCards.RemoveAt(randomCard);
        }
        else {
            player2Cards.Add(eventCard);
            eventCards.RemoveAt(randomCard);
        }
    }
}
