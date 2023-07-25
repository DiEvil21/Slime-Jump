using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject[] platform;

    public int numberOfPlatforms;
    public float levelWidth;
    public float minY;
    public float maxY;
    Vector3 spawnPosition;
    private System.Random _random = new System.Random();

    void Start()
    {
        spawnPosition = new Vector3();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("ground").Length < numberOfPlatforms)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(platform[_random.Next(0, platform.Length)], spawnPosition, Quaternion.identity);
        }
    }
}
