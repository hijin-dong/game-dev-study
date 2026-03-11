using UnityEngine;

public class NamePresenter : MonoBehaviour
{
    public GameObject Cam;
    public TextMesh name;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        name.text = NameInput.playerName;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Cam.transform.rotation;
    }
}
