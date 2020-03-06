using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Code by Vitali Kross

public class TurnController : MonoBehaviour {

    public static int _turn = 1;
    private int _round;
    private int _playersCount = 2;
    private string _player1Nickname = "Player1";
    private string _player2Nickname = "Player2";

    private Text _roundText;
    private Text _turnText;
    private Text _timerText;

    private GameObject _findObject;
    private GameObject _endTurnObject;
    private GameObject _fightField;
    [SerializeField] GameObject _board;

    [SerializeField] private Button _fightEndButton;
    [SerializeField] private Button _diceButtonPlayer1;
    [SerializeField] private Button _diceButtonPlayer2;

    private int _dicePlayer1 = 0;
    private int _dicePlayer2 = 0;

    private float _maxTimer = 30f;
    private float _currentTimer;

    private bool isFight = false;

    [SerializeField]
    private Button _diceButton;

    private string _winPlayerNickname;

    private void Start()
    {
        _findObject = transform.Find("Fight").gameObject;
        _endTurnObject = transform.Find("EndTurn").gameObject;
        _fightField = transform.Find("FightField").gameObject;

        _roundText = transform.Find("Round").GetComponent<Text>();
        _turnText = transform.Find("Turn").GetComponent<Text>();
        _timerText = transform.Find("Time").GetComponent<Text>();

        _fightEndButton.gameObject.SetActive(false);
        _fightField.SetActive(false);
        _roundText.text = "Round: 1";
        _turnText.text = "Turn: " + _player1Nickname;
        _timerText.text = _maxTimer.ToString();
        _timerText.color = Color.white;
        _currentTimer = _maxTimer;
    }
    private void Update()
    {
        if (!isFight) 
        {
            if (_currentTimer > 0) 
            {
                _currentTimer -= 1 * Time.deltaTime;
                _timerText.text = Math.Round(_currentTimer).ToString();

                if (_currentTimer < 5.5)
                {
                    _timerText.color = Color.red;
                }
            }
            else
            {
                EndTurn();
            }
        }
    }
    public void EndTurn() 
    {
        TurnUpdate();
        RoundUpdate();
        TimerUpdate();
        _diceButton.GetComponentInChildren<Text>().text = "Dice";
        _diceButton.interactable = true;
    }
    private void TurnUpdate()
    {
        _turn++;
        _turnText.text = "Turn: " + ((_turn % 2 == 1) ? _player1Nickname : _player2Nickname);
    }
    private void RoundUpdate()
    {
        _round = (int)Math.Ceiling((float)_turn / _playersCount);
        _roundText.text = "Round: " + _round;
    }
    private void TimerUpdate()
    {
        _currentTimer = _maxTimer;
        _timerText.text = _currentTimer.ToString();
        _timerText.color = Color.white;
    }

    public static int GetTurn() {
        return _turn;
    }
    public void Fight() {
        isFight = true;
        _roundText.text = "Fight";
        _turnText.text = _player1Nickname + " VS " + _player2Nickname;
        _timerText.text = "";
        _fightField.SetActive(true);
        _findObject.SetActive(false);
        _endTurnObject.SetActive(false);
        _board.SetActive(false);
        _diceButton.transform.gameObject.SetActive(false);
    }
    public void FightUseDice1() {
        _dicePlayer1 = UnityEngine.Random.Range(1, 6);

        _diceButtonPlayer1.GetComponentInChildren<Text>().text = "Dice: " + _dicePlayer1;
        _diceButtonPlayer1.interactable = false;
        if (_dicePlayer1 >  0 && _dicePlayer2 > 0) {
            _fightEndButton.gameObject.SetActive(true);
        }
    }
    public void FightUseDice2()
    {
        _dicePlayer2 = UnityEngine.Random.Range(1, 6);

        _diceButtonPlayer2.GetComponentInChildren<Text>().text = "Dice: " + _dicePlayer2;
        _diceButtonPlayer2.interactable = false;

        if (_dicePlayer1 > 0 && _dicePlayer2 > 0) {
            _fightEndButton.gameObject.SetActive(true);
        }
    }
    public void FightEnd()
    {

        if (_dicePlayer1 != _dicePlayer2)
        {
            _winPlayerNickname = (_dicePlayer1 > _dicePlayer2) ? _player1Nickname : _player2Nickname; ;
        }
        else
        {
            _winPlayerNickname = "Nobody";
        }
        Player.winPlayerNickname = _winPlayerNickname;

        SceneManager.LoadScene("Win");
        //isFight = false;
        //_roundText.text = "Round: " + _round;
        //_turnText.text = "Turn: " + ((_turn % 2 == 1) ? _player1Nickname : _player2Nickname);
        //_timerText.text = _currentTimer.ToString();
        //_fightField.SetActive(false);
        //_findObject.SetActive(true);
        //_endTurnObject.SetActive(true);
        //_board.SetActive(true);
        //_diceButton.transform.gameObject.SetActive(true);
    }
}
