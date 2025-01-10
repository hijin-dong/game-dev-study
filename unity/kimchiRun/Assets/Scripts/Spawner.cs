using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    public float minSpawnDelay;
    public float maxSpawnDelay;
    
    [Header("References")]
    public GameObject[] gameObjects;

    // Start에서 처음 Spawn 호출,
    // 이후부터는 Spawn이 자기 자신을 호출

    void OnEnable() // 기존 Start 함수와 달리 오브젝트가 활성화될 때 호출됨
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void Spawn() 
    {
        GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)];
        Instantiate(randomObject, transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
