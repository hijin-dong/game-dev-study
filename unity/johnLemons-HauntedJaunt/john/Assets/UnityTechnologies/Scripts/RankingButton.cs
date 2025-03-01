using UnityEngine;

public class RankingButton : MonoBehaviour
{
    public void ExitButton()
    {
        Debug.Log("ExitButton");
        Application.Quit();
    }

    public void HomeButton()
    {
        Debug.Log("HomeBUtton");
        LoadScene.LoadStartScene();
    }
}
