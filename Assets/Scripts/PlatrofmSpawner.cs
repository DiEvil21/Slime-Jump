using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatrofmSpawner : MonoBehaviour
{
    public GameObject[] platforms;
    public float spawnDistance;
    public float hDifference;
    public GameObject player;
    private float lastH;
    private System.Random _random = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y + spawnDistance > lastH) 
        {
            GameObject go = Instantiate(platforms[_random.Next(0, platforms.Length)], new Vector3(0,lastH + hDifference,0), Quaternion.identity);
            lastH = go.transform.position.y;
        }
    }
}
