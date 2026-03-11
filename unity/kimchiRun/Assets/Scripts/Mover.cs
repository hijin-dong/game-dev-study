using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.CalculateGameSpeed() / 5 * Time.deltaTime;
    }
}
