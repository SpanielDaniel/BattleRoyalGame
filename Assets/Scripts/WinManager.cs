using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinManager : MonoBehaviour {
    private float time = 5f;
    [SerializeField] private Text WinText;
    private void Start() {
        WinText.text = Player.winPlayerNickname + " Wins!";
    }
    void Update() {
        time -= Time.deltaTime;
        if (time < 0) {
            SceneManager.LoadScene("Menu");
        }
    }
}
