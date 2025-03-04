using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public static int selectedLevel;
    public int level;
    public TextMeshProUGUI levelButtonText;
    public TextMeshProUGUI levelText;
    public GameManager gameManager;


    void Start()
    {
        levelButtonText.text = level.ToString();
    }

    public void OpenScene()
    {
        selectedLevel = level;
        levelText.text = "Level " + level.ToString();
        selectedLevel = level;
        SceneManager.LoadScene("Placement Puzzle");
    }

    
}
