using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite OnHeart;
    public Sprite OffHeart;
    public int LiveNumber;
    public SpriteRenderer SpriteRenderer;
    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.Instance.Lives >= LiveNumber)
            SpriteRenderer.sprite = OnHeart;
        else
            SpriteRenderer.sprite = OffHeart;
    }
}
