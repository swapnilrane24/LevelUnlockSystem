using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles level button functions
/// </summary>
namespace LevelUnlockSystem
{
    public class LevelButtonScript : MonoBehaviour
    {
        [SerializeField] private GameObject lockObj, unlockObj;     //ref to lock and unlock gameobject
        [SerializeField] private Image[] starsArray;                //ref to all the stars of button
        [SerializeField] private Text levelIndexText;               //ref to text which indicate the level number
        [SerializeField] private Color lockColor, unlockColor;      //color references
        [SerializeField] private Button btn;                        //ref to hold button component
        [SerializeField] private GameObject activeLevelIndicator;

        private int levelIndex;                                     //int which hold the level Index this perticular button specify

        private void Start()
        {
            btn.onClick.AddListener(() => OnClick());               //add listener to the button
        }

        /// <summary>
        /// Method to set the button
        /// </summary>
        /// <param name="value">Level Data</param>
        /// <param name="index">Level index</param>
        /// /// <param name="activeLevel">Bool used to identify the active level</param>
        public void SetLevelButton(LevelItem value, int index, bool activeLevel)        
        {
            if (value.unlocked)                                     //if unlocked is true
            {
                activeLevelIndicator.SetActive(activeLevel);
                levelIndex = index + 1;                             //set levelIndex, Note: We add 1 because array start from 0 and level index start from 1 
                btn.interactable = true;                            //make button interactable
                lockObj.SetActive(false);                           //deactivate lockObj
                unlockObj.SetActive(true);                          //activate unlockObj
                SetStar(value.starAchieved);                        //set the stars
                levelIndexText.text = "" + levelIndex;              //set levelIndexText text

            }
            else
            {
                btn.interactable = false;                           //remove button interactable
                lockObj.SetActive(true);                            //activate lockObj
                unlockObj.SetActive(false);                         //deactivate unlockObj
            }
        }

        /// <summary>
        /// Method to show number of stars achieved by the player for this perticular level
        /// </summary>
        /// <param name="starAchieved"></param>
        private void SetStar(int starAchieved)
        {
            for (int i = 0; i < starsArray.Length; i++)             //loop through entire star array
            {
                /// <summary>
                /// if i is less than starAchieved
                /// Eg: if 2 stars are achieved we set the start at index 0 and 1 color to unlockColor, as array start from 0 element
                /// </summary>
                if (i < starAchieved)                               
                {
                    starsArray[i].color = unlockColor;              //set its color to unlockColor
                }
                else
                {
                    starsArray[i].color = lockColor;                //else set its color to lockColor
                }
            }
        }

        void OnClick()                                              //method called by button
        {
            LevelSystemManager.Instance.CurrentLevel = levelIndex - 1;  //set the CurrentLevel, we subtract 1 as level data array start from 0
            SceneManager.LoadScene("Level" + levelIndex);           //load the level
        }

    }
}