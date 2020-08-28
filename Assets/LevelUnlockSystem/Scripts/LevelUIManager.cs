using UnityEngine;

/// <summary>
/// This script create the grid of level buttons in LevelMenu
/// </summary>
namespace LevelUnlockSystem
{
    public class LevelUIManager : MonoBehaviour
    {
        [SerializeField] private LevelButtonScript levelBtnPrefab;              //ref to LevelButton prefab
        [SerializeField] private Transform levelBtnGridHolder;                  //ref to grid holder

        private void Start()
        {
            InitializeUI();
        }

        private void InitializeUI()                                             //method to create the level buttons
        {
            LevelData[] levelDatas = LevelSystemManager.Instance.LevelsData.levelData;  //get the level data array

            for (int i = 0; i < levelDatas.Length; i++)                         //loop through entire array
            {
                LevelButtonScript levelButton = Instantiate(levelBtnPrefab, levelBtnGridHolder);    //create button for each element in array
                                                                                //and set the button
                levelButton.SetLevelButton(levelDatas[i], i, i == LevelSystemManager.Instance.LevelsData.lastUnlockedLevel);                      
            }
        }
    }
}