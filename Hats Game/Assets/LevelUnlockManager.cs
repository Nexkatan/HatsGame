using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockManager : MonoBehaviour
{

    public int levelsUnlocked;

    public Button[] levelButtons;

    void Start()
    { 
        SetLevels();
    }
    
    public void SetLevels()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
        for (int i = 0; i < levelsUnlocked; i++)
        {
            levelButtons[i].interactable = true;
        }
    }

    public void ResetLevels()
    {
        PlayerPrefs.SetInt("levelsUnlocked", 1);
        SetLevels();
    }
}
