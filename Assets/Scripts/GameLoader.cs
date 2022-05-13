using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    int levelToLoad = 0;

    private void Awake()
    {
        levelToLoad = PlayerPrefs.GetInt("savedLevel", 1);
    }

    private void Start()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
