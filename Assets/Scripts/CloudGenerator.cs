using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public GameObject[] clouds;

    public int numberOfClouds;
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
        if (GameObject.FindGameObjectsWithTag("cloud").Length < numberOfClouds)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            GameObject go = Instantiate(clouds[_random.Next(0,clouds.Length)], spawnPosition, Quaternion.identity);
            go.transform.localScale += new Vector3(1, 1, 1) * _random.Next(15, 30);
        }
    }
}
