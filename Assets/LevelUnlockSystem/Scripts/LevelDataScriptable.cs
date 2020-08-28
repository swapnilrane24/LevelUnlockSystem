using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script used to create LevelData scriptable object
/// </summary>
namespace LevelUnlockSystem
{
    [CreateAssetMenu(fileName = "LevelList", menuName = "LevelUnlockSystem/LevelList", order = 0)]
    public class LevelDataScriptable : ScriptableObject
    {
        public int lastUnlockedLevel = 0;   //has referece to lastUnlockedLevel
        public LevelData[] levelData;       //reference to level data
    }

    [System.Serializable]
    public class LevelData                  //level data
    {
        public bool unlocked;
        public int starAchieved;
    }
}