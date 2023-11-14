using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool tileSelected;
    public GameObject selectedTile;
    public bool gameOver = false;
    public int difficulty = 2;
    public Slider _slider;
    public TextMeshProUGUI sliderText;
    public static int tilingDifficulty;


    public void Start()
    {
        if (_slider != null)
        {
            _slider.onValueChanged.AddListener(value =>
            {
                tilingDifficulty = (int)value;
                sliderText.text = value.ToString();
            });
        }
    }
    public void LoadHome()
    {
        SceneManager.LoadScene("MainScreen");
    }
   public void LoadHoleInTheWall()
    {
        SceneManager.LoadScene("Hole In The Wall");
    }
    public void LoadLevelEditor()
    {
        SceneManager.LoadScene("Level Editor");
    }
    public void LoadPlacementPuzzle()
    {
        SceneManager.LoadScene("Placement Puzzle Level Selector");
    }
    public void LoadTilingFill()
    {
        SceneManager.LoadScene("Tiling Fill");

    }
   
}
