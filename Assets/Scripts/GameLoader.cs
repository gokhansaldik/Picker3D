using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    private int _levelToLoad = 0;
    private void Awake()
    {
        _levelToLoad = PlayerPrefs.GetInt("savedLevel", 1);
    }
    private void Start()
    {
        SceneManager.LoadScene(_levelToLoad);
    }
}
