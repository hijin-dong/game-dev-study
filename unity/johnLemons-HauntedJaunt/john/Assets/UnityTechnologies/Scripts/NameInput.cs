using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameInput : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public bool isClicked = false;
    public string playerName = "";

    // Update is called once per frame
    void Update()
    {
        playerName = playerNameInput.text;
        if (isClicked)
        {
            if (playerName.Length > 0)
                GameStartButton.LoadGameScene();
            else
                isClicked = false;
        }   
    }

    public void SetIsClicked()
    {
        isClicked = true;
    }
}
