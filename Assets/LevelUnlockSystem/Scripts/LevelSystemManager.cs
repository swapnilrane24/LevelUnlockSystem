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

        [SerializeField] private LevelDataScriptable levelScriptableData;       //hold the scriptable level data
        public LevelDataScriptable LevelsData { get => levelScriptableData; }   //getter

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

        public void LevelComplete(int starAchieved)                             //method called when player win the level
        {
            levelScriptableData.levelData[currentLevel].starAchieved = starAchieved;    //save the stars achieved by the player in level
            levelScriptableData.lastUnlockedLevel = currentLevel + 1;           //change the lastUnlockedLevel to next level
            levelScriptableData.levelData[levelScriptableData.lastUnlockedLevel].unlocked = true; //and make next level unlock true
        }
    }
}