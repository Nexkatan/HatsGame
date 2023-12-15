using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PipeDreamMainMenu : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI scoreLabel;
    public GameObject initialBG;

    public void StartGame(int mode)
    {
        player.gameObject.SetActive(true);
        player.StartGame(mode);
        gameObject.SetActive(false);
        Cursor.visible = false;
        initialBG.SetActive(false);
    }

    public void EndGame(float distanceTravelled)
    {
        scoreLabel.text = ((int)distanceTravelled * 10f).ToString();
        gameObject.SetActive(true);
        Cursor.visible = true;
    }
}
