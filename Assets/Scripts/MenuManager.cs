using UnityEngine;
using UnityEngine.SceneManagement;
//Code by Vitali Kross

public class MenuManager : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("Character");
    }

    public void Exit() {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }
}
