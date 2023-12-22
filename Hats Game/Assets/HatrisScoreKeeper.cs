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

    public Material player1Mat;
    public Material player2Mat;

    public int playerCount = 0;

    public HexGrid grid;

    private void Start()
    {
        team1Score.text = score1.ToString();
        team2Score.text = score2.ToString();
        scores[0] = score1;
        scores[1] = score2;

        grid = GameObject.FindObjectOfType<HexGrid>();


        foreach (HexCell cell in grid.cells)
        {
            if (cell.isHatrisCell)
            {

                Debug.Log("Cell");
            }
        }
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

    public void KeepScore()
    {
        score1 = 0;
        score2 = 0;
        foreach (HexCell cell in grid.cells)
        {
            if (cell.isHatrisCell)
            {
               if (cell.playerCellScored == 1)
                {
                    score1++;
                }
               else if (cell.playerCellScored == 2)
                {
                    score2++;
                }
            }
        }
        team1Score.text = score1.ToString();
        team2Score.text = score2.ToString();
    }
}
