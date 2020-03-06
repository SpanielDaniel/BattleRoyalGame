using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//Code by Vitali Kross

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
