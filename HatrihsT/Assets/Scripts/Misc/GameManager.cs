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


    public Slider tilingFill_slider;
    public TextMeshProUGUI tilingFill_sliderText;
    public static int tilingDifficulty = 1;

    public static bool tilingFillColourMode;
    public static bool AIMode;

    public Slider hatris_slider;
    public TextMeshProUGUI hatrisSliderText;

    [SerializeField]
    public static int hatrisBoardSize = 3;

    public void Start()
    {
        if (tilingFill_slider != null)
        {
            tilingFill_slider.onValueChanged.AddListener(value =>
            {
                tilingDifficulty = (int)value;
                tilingFill_sliderText.text = value.ToString();
            });
        }

        if (hatris_slider != null)
        {
            hatris_slider.onValueChanged.AddListener(value =>
            {
                hatrisBoardSize = (int)value;
                hatrisSliderText.text = value.ToString();
            });
        }
    }

    public void LoadHome()
    {
        SceneManager.LoadScene("MainScreen");
        tilingDifficulty = 1;
        tilingFillColourMode = false;
        AIMode = false;
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
    public void LoadPipeDream()
    {
        SceneManager.LoadScene("Pipe Dream");
    }

    public void LoadHatris()
    {
        SceneManager.LoadScene("Hatris");
    }

    public void ColourModeAction()
    {
        if (tilingFillColourMode)
        {
            tilingFillColourMode = false;
        }
        else
        {
            tilingFillColourMode = true;
        }
    }
    public void AIModeAction()
    {
        if (AIMode)
        {
            AIMode = false;
        }
        else
        {
            AIMode = true;
        }
    }
}
