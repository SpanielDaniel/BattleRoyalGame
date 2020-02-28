using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour {

    private int _turn = 1;
    private int _round;
    private int _playersCount = 2;
    private string _player1Nickname = "Player1";
    private string _player2Nickname = "Player2";

    private Text _roundText;
    private Text _turnText;
    private Text _timerText;

    private float _maxTimer = 30f;
    private float _currentTimer;

    [SerializeField]
    private Button _diceButton;

    private void Start() {
        _roundText = transform.Find("Round").GetComponent<Text>();
        _turnText = transform.Find("Turn").GetComponent<Text>();
        _timerText = transform.Find("Time").GetComponent<Text>();

        _roundText.text = "Round: 1";
        _turnText.text = "Turn: " + _player1Nickname;
        _timerText.text = _maxTimer.ToString();
        _timerText.color = Color.white;
        _currentTimer = _maxTimer;
    }
    private void Update() {
        if (_currentTimer > 0) {
            _currentTimer -= 1 * Time.deltaTime;
            _timerText.text = Math.Round(_currentTimer).ToString();

            if (_currentTimer < 5.5) {
                _timerText.color = Color.red;
            }
        }
        else {
            EndTurn();
        }
    }
    public void EndTurn() {
        TurnUpdate();
        RoundUpdate();
        TimerUpdate();
        _diceButton.GetComponentInChildren<Text>().text = "Dice";
        _diceButton.interactable = true;
    }
    private void TurnUpdate() {
        _turn++;
        _turnText.text = "Turn: " + ((_turn % 2 == 1) ? _player1Nickname : _player2Nickname);
    }
    private void RoundUpdate() {
        _round = (int) Math.Ceiling((float)_turn / _playersCount);
        _roundText.text = "Round: " + _round;
    }
    private void TimerUpdate() {
        _currentTimer = _maxTimer;
        _timerText.text = _currentTimer.ToString();
        _timerText.color = Color.white;
    }
}
