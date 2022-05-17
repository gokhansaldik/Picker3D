using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool ShowTitle = true;
    public static bool GameComplete = false;
    private bool _gameStarted = false;
    public int CurrentLevelNo = 1;
    private int _nextLevelNo = 2;
    private int _maxLevelNo = 10;
    private float _levelFailTimer = 5f;
    private bool _levelFailTimerStart = false;
    private bool _levelFailed = false;
    private bool _levelComplete = false;
    public GameObject TitleScreen;
    public GameObject TapToStart;
    public GameObject LevelFailed;
    public GameObject LevelComplete;
    public GameObject LevelProgression;
    public Text Canvas_currentLevelNo;
    public Text Canvas_nextLevelNo;
    public Color LevelColor;
    public Renderer[] Grounds;

    private void Awake()
    {
        PaintLevel();
        if ((!GameComplete)&&(CurrentLevelNo < _maxLevelNo))
        {
            _nextLevelNo = CurrentLevelNo + 1;
        }
        else
        {
            _nextLevelNo = ChooseRandomLevel();
        }
        Canvas_currentLevelNo.text = CurrentLevelNo.ToString();
        Canvas_nextLevelNo.text = _nextLevelNo.ToString();
        LevelFailed.SetActive(false);
        LevelComplete.SetActive(false);
        if (ShowTitle)
        {
            TitleScreen.SetActive(true);
            LevelProgression.SetActive(false);
        }
        else
        {
            TitleScreen.SetActive(false);
            LevelProgression.SetActive(true);
        }
    }

    private void Update()
    {
        DebugControls();
        if (!_gameStarted)
        {
            if (Input.GetMouseButton(0))
            {
                _gameStarted = true;
                if (ShowTitle)
                {
                    ShowTitle = false;
                    TitleScreen.SetActive(false);
                    LevelProgression.SetActive(true);
                }

                TapToStart.SetActive(false);
                FindObjectOfType<PlayerController>().IsMoving = true;
            }
        }
        else if (_levelFailed)
        {
            if (Input.GetMouseButton(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else if (_levelComplete)
        {
            if (Input.GetMouseButton(0))
            {
                PlayerPrefs.SetInt("savedLevel", _nextLevelNo);
                SceneManager.LoadScene(_nextLevelNo);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_levelFailTimerStart)
        {
            if (_levelFailTimer > 0)
            {
                _levelFailTimer -=  Time.fixedDeltaTime;
            }
            else
            {
                _levelFailTimerStart = false;
                LevelFailed.SetActive(true);
                _levelFailed = true;
                Debug.Log("Level Failed!");
            }
        }
    }

    public void toggleFailTimer(bool state)
    {
        if (state)
        {
            _levelFailTimer = 6f;
            _levelFailTimerStart = true;
        }
        else
        {
            _levelFailTimerStart = false;
        }
    }

    public void showLevelCompleteScreen()
    {
        _levelComplete = true;
        if ((!GameComplete) && (CurrentLevelNo == _maxLevelNo))
        {
            GameComplete = true;
            Debug.Log("Random level progression has started.");
        }
        LevelProgression.SetActive(false);
        LevelComplete.SetActive(true);
        FindObjectOfType<CameraController>().Target = null;
    }

    void PaintLevel()
    {
        for (int i = 0; i < Grounds.Length; i++)
        {
            Grounds[i].material.color = LevelColor;
        }
        Goal[] goals = FindObjectsOfType<Goal>();
        for (int i = 0; i < goals.Length; i++)
        {
            goals[i].MainPlatformEndColor = LevelColor;
            goals[i].UnitCompleteColor = LevelColor;
        }
        Canvas_currentLevelNo.transform.parent.gameObject.GetComponent<Image>().color = LevelColor;
    }

    int ChooseRandomLevel()
    {
        int result = Random.Range(1, _maxLevelNo);
        if (result == CurrentLevelNo)
        {
            result += 1;
            if (result > _maxLevelNo)
            {
                result = 1;
            }
        }
        return result;
    }

    void DebugControls()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Player save data has been reset.");
            PlayerPrefs.SetInt("savedLevel", 1);
            GameManager.GameComplete = false;
        }
    }

}
