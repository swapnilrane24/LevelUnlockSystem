using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script hold the level data scriptable object and its Singleton and dont get deleted on scene change
/// </summary>
namespace LevelUnlockSystem
{
    public class LevelSystemManager : MonoBehaviour
    {
        private static LevelSystemManager instance;                             //instance variable
        public static LevelSystemManager Instance { get => instance; }          //instance getter

        [Tooltip("Set the default Level data so when game start 1st time, this data will be saved")]
        [SerializeField] private LevelData levelData;

        public LevelData LevelData { get => levelData; }   //getter

        private int currentLevel;                                               //keep track of current level player is playing
        public int CurrentLevel { get => currentLevel; set => currentLevel = value; }   //getter and setter for currentLevel


        private void Awake()
        {
            if (instance == null)                                               //if instance is null
            {
                instance = this;                                                //set this as instance
                DontDestroyOnLoad(gameObject);                                  //make it DontDestroyOnLoad
            }
            else
            {
                Destroy(gameObject);                                            //else destroy it
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SaveLoadData.Instance.SaveData();
            }
        }

        private void OnEnable()
        {
            SaveLoadData.Instance.Initialize();
        }

        public void LevelComplete(int starAchieved)                             //method called when player win the level
        {
            levelData.levelItemArray[currentLevel].starAchieved = starAchieved;    //save the stars achieved by the player in level
            if (levelData.lastUnlockedLevel < (currentLevel + 1))
            {
                levelData.lastUnlockedLevel = currentLevel + 1;           //change the lastUnlockedLevel to next level
                                                                          //and make next level unlock true
                levelData.levelItemArray[levelData.lastUnlockedLevel].unlocked = true;
            }
        }
    }
}