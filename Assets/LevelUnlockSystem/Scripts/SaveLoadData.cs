using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelUnlockSystem
{
    /// <summary>
    /// This script save and load the LevelData
    /// </summary>
    public class SaveLoadData : MonoBehaviour
    {
        private static SaveLoadData instance;

        public static SaveLoadData Instance { get => instance; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        //Method to initialize the SaveLoad Script
        public void Initialize()
        {
            //ClearData();
            if (PlayerPrefs.GetInt("GameStartFirstTime") == 1)  //if PlayerPrefs of "GameStartFirstTime" value is 1, means we are playing the game again
            {
                LoadData();                                     //so we load the data
            }
            else                                                //if its not 1, means we are playing the game 1st time
            {
                SaveData();                                     //save the data 1st
                PlayerPrefs.SetInt("GameStartFirstTime", 1);    //save the PlayerPrefs
            }
        }

        //this is Unity method which is called when game is crashed or in background or quit
        private void OnApplicationPause(bool pause)
        {
            SaveData();                                 //great for saving the data. Note: In editor doesnt work correctly, but works great in Build
        }

        /// <summary>
        /// Method used to save the data
        /// </summary>
        public void SaveData()
        {
            //convert the data to string
            string levelDataString = JsonUtility.ToJson(LevelSystemManager.Instance.LevelData);

            try
            {
                //save the string as json 
                System.IO.File.WriteAllText(Application.persistentDataPath + "/LevelData.json", levelDataString);
                Debug.Log("Data Saved");

            }
            catch (System.Exception e)
            {
                //if we get any error debug it
                Debug.Log("Error Saving Data:" + e);
                throw;
            }
        }

        //Method used to load the data
        private void LoadData()
        {
            try
            {
                //get the text data from json and stro it in string
                string levelDataString = System.IO.File.ReadAllText(Application.persistentDataPath + "/LevelData.json");
                LevelData levelData = JsonUtility.FromJson<LevelData>(levelDataString); //create LevelData from json
                if (levelData != null)
                {
                    //set the LevelData of LevelSystemManager
                    LevelSystemManager.Instance.LevelData.levelItemArray = levelData.levelItemArray;
                    LevelSystemManager.Instance.LevelData.lastUnlockedLevel = levelData.lastUnlockedLevel;
                }
                Debug.Log("Data Loaded");
            }
            catch (System.Exception e)
            {
                Debug.Log("Error Loading Data:" + e);
                throw;
            }
            
        }

        /// <summary>
        /// Method to clear all the save data
        /// </summary>
        public void ClearData()
        {
            Debug.Log("Data Cleared");
            LevelData levelData = new LevelData();
            SaveData();
            PlayerPrefs.SetInt("GameStartFirstTime", 1);
        }


    }

    [System.Serializable]
    public class LevelData
    {
        public int lastUnlockedLevel = 0;   //has referece to lastUnlockedLevel
        public LevelItem[] levelItemArray;       //reference to level data
    }

    [System.Serializable]
    public class LevelItem                  //level item
    {
        public bool unlocked;
        public int starAchieved;
    }
}