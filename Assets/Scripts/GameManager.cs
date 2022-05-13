using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool showTitle = true;
    public static bool gameComplete = false;

    bool gameStarted = false;

    public int currentLevelNo = 1;
    int nextLevelNo = 2;
    int maxLevelNo = 10;

    float levelFailTimer = 5f;
    bool levelFailTimerStart = false;
    bool levelFailed = false;

    bool levelComplete = false;

    [Header("UI Objects")]
    public GameObject canvas_titleScreen;
    public GameObject canvas_tapToStart;
    public GameObject canvas_levelFailed;
    public GameObject canvas_levelComplete;
    public GameObject canvas_levelProgression;
    
    public Text canvas_currentLevelNo;
    public Text canvas_nextLevelNo;

     



    public Color levelColor;
    public Renderer[] grounds;

    private void Awake()
    {
        PaintLevel();
        if ((!gameComplete)&&(currentLevelNo < maxLevelNo))
        {
            nextLevelNo = currentLevelNo + 1;
        }
        else
        {
            nextLevelNo = ChooseRandomLevel();
        }
        canvas_currentLevelNo.text = currentLevelNo.ToString();
        canvas_nextLevelNo.text = nextLevelNo.ToString();
        
        canvas_levelFailed.SetActive(false);
        canvas_levelComplete.SetActive(false);
        if (showTitle)
        {
            canvas_titleScreen.SetActive(true);
            canvas_levelProgression.SetActive(false);
        }
        else
        {
            canvas_titleScreen.SetActive(false);
            canvas_levelProgression.SetActive(true);
        }
    }

    private void Update()
    {
        DebugControls();

        if (!gameStarted)
        {
            if (Input.GetMouseButton(0))
            {
                gameStarted = true;
                if (showTitle)
                {
                    showTitle = false;
                    canvas_titleScreen.SetActive(false);
                    canvas_levelProgression.SetActive(true);
                }

                canvas_tapToStart.SetActive(false);
                FindObjectOfType<PlayerController>().isMoving = true;
            }
        }
        else if (levelFailed)
        {
            if (Input.GetMouseButton(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else if (levelComplete)
        {
            if (Input.GetMouseButton(0))
            {
                PlayerPrefs.SetInt("savedLevel", nextLevelNo);
                SceneManager.LoadScene(nextLevelNo);
            }
        }



    }

    private void FixedUpdate()
    {
        if (levelFailTimerStart)
        {
            if (levelFailTimer > 0)
            {
                levelFailTimer -=  Time.fixedDeltaTime;
            }
            else
            {
                levelFailTimerStart = false;
                canvas_levelFailed.SetActive(true);
                levelFailed = true;
                Debug.Log("Level Failed!");
            }
        }
    }

    public void toggleFailTimer(bool state)
    {
        if (state)
        {
            levelFailTimer = 6f;
            levelFailTimerStart = true;
        }
        else
        {
            levelFailTimerStart = false;
        }
        
    }

    public void showLevelCompleteScreen()
    {
        levelComplete = true;
        if ((!gameComplete) && (currentLevelNo == maxLevelNo))
        {
            gameComplete = true;
            Debug.Log("Random level progression has started.");
        }
        

        canvas_levelProgression.SetActive(false);
        canvas_levelComplete.SetActive(true);

        FindObjectOfType<CameraController>().target = null;
    }

    void PaintLevel()
    {
        for (int i = 0; i < grounds.Length; i++)
        {
            grounds[i].material.color = levelColor;
        }
        Goal[] goals = FindObjectsOfType<Goal>();
        for (int i = 0; i < goals.Length; i++)
        {
            goals[i].mainPlatform_endColor = levelColor;
            goals[i].unitCompleteColor = levelColor;
        }
        canvas_currentLevelNo.transform.parent.gameObject.GetComponent<Image>().color = levelColor;
    }

    int ChooseRandomLevel()
    {
        int result = Random.Range(1, maxLevelNo);
        if (result == currentLevelNo)
        {
            result += 1;
            if (result > maxLevelNo)
            {
                result = 1;
            }
        }
        return result;
    }

    void DebugControls()
    {
        //Reset Game
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Player save data has been reset.");
            PlayerPrefs.SetInt("savedLevel", 1);
            GameManager.gameComplete = false;
        }
    }

}
