using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameLevel/LevelsGame", fileName = "NewLevel")]
    public class LevelSO : ScriptableObject
    {
   
        public int CurrrentLevel;
        public List<Level> GameLevel;
    }
    [System.Serializable]
    public class Level
    {
        public int HowManyRoad;
        public int HowManyCheckpoint;
    }

