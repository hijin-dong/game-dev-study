using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Scenes/MainScene");
        Debug.Log("Main Scene");
    }
}