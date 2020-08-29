using UnityEngine;

/// <summary>
/// This script create the grid of level buttons in LevelMenu
/// </summary>
namespace LevelUnlockSystem
{
    public class LevelUIManager : MonoBehaviour
    {
        private static LevelUIManager instance;                             //instance variable
        public static LevelUIManager Instance { get => instance; }          //instance getter

        [SerializeField] private LevelButtonScript levelBtnPrefab;              //ref to LevelButton prefab
        [SerializeField] private Transform levelBtnGridHolder;                  //ref to grid holder

        private void Start()
        {
            InitializeUI();
        }

        private void Awake()
        {
            if (instance == null)                                               //if instance is null
            {
                instance = this;                                                //set this as instance
            }
            else
            {
                Destroy(gameObject);                                            //else destroy it
            }
        }

        public void InitializeUI()                                             //method to create the level buttons
        {
            LevelItem[] levelItemsArray = LevelSystemManager.Instance.LevelData.levelItemArray;  //get the level data array

            for (int i = 0; i < levelItemsArray.Length; i++)                         //loop through entire array
            {
                LevelButtonScript levelButton = Instantiate(levelBtnPrefab, levelBtnGridHolder);    //create button for each element in array
                                                                                //and set the button
                levelButton.SetLevelButton(levelItemsArray[i], i, i == LevelSystemManager.Instance.LevelData.lastUnlockedLevel);                      
            }
        }
    }
}