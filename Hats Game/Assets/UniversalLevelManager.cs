using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UniversalLevelManager : MonoBehaviour
{
    public GameObject LevelCompleteButtons;
    public GameObject HatTab;
    public GameObject levelWalls;
    public ChainManager chainManager;
    public TextMeshProUGUI levelText;

    private void Start()
    {
        int levelSelected = LevelSelector.selectedLevel;
        if (levelSelected > 0 && levelWalls.transform.childCount >= levelSelected)
        {
            levelWalls.transform.GetChild(levelSelected - 1).gameObject.SetActive(true);
        }
        levelText.text = "LEVEL " + levelSelected;
    }
   
    public void RedoLevel()
    {
        SceneManager.LoadScene("Placement Puzzle");
    }

    public void NextLevel()
    {
        LevelSelector.selectedLevel += 1;
        SceneManager.LoadScene("Placement Puzzle");
    }

    public void LevelComplete()
    {
        LevelCompleteButtons.SetActive(true);
        HatTab.SetActive(false);
        if (LevelSelector.selectedLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", LevelSelector.selectedLevel + 1); 
        }
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("Placement Puzzle Level Selector");
    }


}
