using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static void LoadGameScene()
    {
        SceneManager.LoadScene("Scenes/MainScene");
    }

    public static void LoadStartScene()
    {
        SceneManager.LoadScene("Scenes/StartScene");
    }
}