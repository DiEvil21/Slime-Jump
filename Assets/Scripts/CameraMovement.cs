using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    public GameObject score_label;
    private Vector3 stageDimensions;

    void Start()
    {
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (target.transform.position.y > transform.position.y) 
        {
            transform.position = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
        }
        score_label.GetComponent<Text>().text = (int)transform.position.y + "";
        GameObject.FindGameObjectWithTag("player").GetComponent<PlayeMovement>().saveProgress((int)transform.position.y);
        
    }
}
