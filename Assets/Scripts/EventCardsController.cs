using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventCardsController : MonoBehaviour
{
    [SerializeField] private GameObject _eventCards;
    [SerializeField] private GameObject _closeButton;
    [SerializeField] private GameObject _openButton;

    void Start()
    {
        _eventCards.SetActive(false);
        _closeButton.SetActive(false);
        _openButton.SetActive(true);
    }

    public void CloseButton() {
        _eventCards.SetActive(false);
        _closeButton.SetActive(false);
        _openButton.SetActive(true);
    }
    public void OpenButton() {
        _eventCards.SetActive(true);
        _closeButton.SetActive(true);
        _openButton.SetActive(false);
    }
}
