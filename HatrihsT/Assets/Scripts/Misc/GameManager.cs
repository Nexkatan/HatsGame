using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public bool tileSelected;
    public GameObject selectedTile;
    public bool gameOver = false;

    public static bool AIMode;

    public GameObject MainScreen;
    public Slider hatris_slider;
    public TextMeshProUGUI hatrisSliderText;

    public HexGrid grid;
    public HatrisScoreKeeper scoreKeeper;

    [SerializeField]
    public static int hatrisBoardSize = 3;

    public void Start()
    {
        if (hatris_slider != null)
        {
            hatris_slider.onValueChanged.AddListener(value =>
            {
                hatrisBoardSize = (int)value;
                hatrisSliderText.text = value.ToString();
            });
        }
    }

    public void LoadGame()
    {
        grid.LoadHatrisHex();
        scoreKeeper.LoadNewGame();
        MainScreen.SetActive(false);
    }

    public void ReloadGame()
    {
        scoreKeeper.ResetGame();
        LoadGame();
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
