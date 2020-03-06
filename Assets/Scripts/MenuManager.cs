using UnityEngine;
using UnityEngine.SceneManagement;
//Code by Vitali Kross

public class MenuManager : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("Test1");
    }

    public void Exit() {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }
}
