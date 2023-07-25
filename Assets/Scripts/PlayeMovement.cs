using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using YG;

public class PlayeMovement : MonoBehaviour
{
    public float jumpHeight = 7f;
    public GameObject startMenu;
    public GameObject restartMenu;
    private Rigidbody2D rb;
    private Vector3 touchPos;
    private Vector3 direction;
    private float moveSpeed = 1f;
    private Vector3 defaultScale;
    public GameObject score_label;
    private bool isPlay = false;
    public GameObject bestScoreLabel;
    public int best_score;
    private int score;

    private void OnEnable() => YandexGame.GetDataEvent += GetData;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Awake()
    {
        // Проверяем запустился ли плагин
        if (YandexGame.SDKEnabled == true)
        {
            // Если запустился, то запускаем Ваш метод
            GetData();

            // Если плагин еще не прогрузился, то метод не запуститься в методе Start,
            // но он запустится при вызове события GetDataEvent, после прогрузки плагина
        }
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        defaultScale = transform.localScale;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;

    }

    // Update is called once per frame
    void Update()
    {


        if (isPlay) 
        {
            /*if (Input.touchCount > 0 && !IsTouchOverUI(Input.GetTouch(0).fingerId))
            {
                Touch touch = Input.GetTouch(0);
                touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchPos.z = 0;
                direction = (touchPos - transform.position);
                rb.velocity = new Vector2(direction.x * 5, rb.velocity.y) * moveSpeed;
            }*/
            if (Input.GetMouseButton(0) && !IsMouseOverUI())
            {
                touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                touchPos.z = 0;
                direction = (touchPos - transform.position);
                rb.velocity = new Vector2(direction.x, rb.velocity.y) * moveSpeed;
            }
        }
        
    }
    bool IsMouseOverUI()
    {
        // Получаем текущий EventSystem
        EventSystem eventSystem = EventSystem.current;

        // Проверяем, было ли нажатие на UI
        if (eventSystem != null)
        {
            // Метод IsPointerOverGameObject() возвращает true, если курсор находится над UI элементом
            return eventSystem.IsPointerOverGameObject();
        }

        return false;
    }
    
    private void Explose() 
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }

    private void playJumpSound() 
    {
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        Debug.Log("hello");
        if (GetComponent<Rigidbody2D>().velocity.y < 0) 
        {
            if (col.gameObject.CompareTag("ground"))
            {
                playJumpSound();
                Explose();
                GetComponent<Animator>().SetBool("jump", true);
                //GetComponent<Animation>().Play();
                var currentVel = rb.velocity;
                var newVel = new Vector3(currentVel.x, jumpHeight, currentVel.y);
                rb.velocity = newVel;
                //transform.localScale = new Vector3(defaultScale.x, defaultScale.y * 0.8f, defaultScale.z);
            }
            if (col.gameObject.CompareTag("jumper"))
            {
                playJumpSound();
                Explose();
                GetComponent<Animator>().SetBool("jump", true);
                var currentVel = rb.velocity;
                var newVel = new Vector3(currentVel.x, 12f, currentVel.y);
                rb.velocity = newVel;
                //transform.localScale = new Vector3(defaultScale.x, defaultScale.y * 0.8f, defaultScale.z);
            }


        }
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        
        transform.localScale = new Vector3(defaultScale.x,defaultScale.y * 1.2f,defaultScale.z);
        
    }

    public void startGame() 
    {
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        isPlay = true;
        rb.velocity = new Vector2(0, rb.velocity.y) * moveSpeed;;
        startMenu.SetActive(false);
        score_label.SetActive(true);
        
    }
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void GetData()
    {
        best_score = YandexGame.savesData.money;
        Debug.Log(YandexGame.savesData.money);
        bestScoreLabel.GetComponent<Text>().text = ":" + best_score.ToString();

    }


    private void OnBecameInvisible()
    {
        Camera mainCamera = Camera.main;
        float bottomCameraY = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        if (transform.position.y < bottomCameraY && transform.position.x >= mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect && transform.position.x <= mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect)
        {
            Debug.Log("Player Invisible");
            transform.GetChild(0).GetComponent<AudioSource>().Play();
            try
            {
                restartMenu.SetActive(true);
            }
            catch { }
        }
    }

    public void saveProgress(int score) 
    {
        if (score > best_score)
        {
            best_score = score;
            YandexGame.savesData.money = best_score;
            bestScoreLabel.GetComponent<Text>().text = ":" + best_score.ToString();
            YandexGame.SaveProgress();
        }
    }
}
