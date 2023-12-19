using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HatrisScoreKeeper : MonoBehaviour
{
    public TextMeshProUGUI team1Score;
    public TextMeshProUGUI team2Score;
    private TextMeshProUGUI[] scoreTexts = new TextMeshProUGUI[2];


    public int score1 = 0;
    public int score2 = 0;

    private int[] scores = new int[2];

    public HatrisHatPlacer player1;
    public HatrisHatPlacer player2;

    public int playerCount = 0;

    private void Start()
    {
        team1Score.text = score1.ToString();
        team2Score.text = score2.ToString();
        scores[0] = score1;
        scores[1] = score2;
    }

    public void AddScore(int scoreToAdd)
    {
        Debug.Log("Score Add");
        if (playerCount == 0)
        {
            score1 = score1 + scoreToAdd;
            team1Score.text = score1.ToString();
        }
        else
        {
            score2 = score2 + scoreToAdd;
            team2Score.text = score2.ToString();
        }
        Debug.Log(scores[playerCount]);
    }
}
