using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.transform.position.y - 10 > transform.position.y) 
        {
            Destroy(gameObject);
        }
        
    }
    public void OnBecameInvisible()
    {
        Camera mainCamera = Camera.main; // Получаем ссылку на главную камеру
        float bottomCameraY = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y; // Преобразуем точку (0, 0) экрана в мировые координаты и получаем y-компоненту

        
        if (transform.position.y < bottomCameraY)
        {
            Destroy(gameObject);
        }
    }
}
