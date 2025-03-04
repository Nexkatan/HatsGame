using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool tileSelected;
    public GameObject selectedTile;
    public bool gameOver = false;

    public static bool AIMode;

    public Slider hatris_slider;
    public TextMeshProUGUI hatrisSliderText;

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

    public void LoadHatris()
    {
        SceneManager.LoadScene("Hatris");
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
