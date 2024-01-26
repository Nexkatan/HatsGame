using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class HatrisScoreKeeper : MonoBehaviour
{
    public TextMeshProUGUI team1Score;
    public TextMeshProUGUI team2Score;

    public int score1 = 0;
    public int score2 = 0;

    private int[] scores = new int[2];

    public HatrisHatPlacer player1;
    public HatrisHatPlacer player2;

    public Material player1Mat;
    public Material player2Mat;

    public int playerCount = 0;

    public HexGrid grid;
    public HatrisHexCell[] cellCells;
    public List<HexCell> cellList;

    public Material origin;
    public Material potentialMat;

    private void Start()
    {
        team1Score.text = score1.ToString();
        team2Score.text = score2.ToString();
        scores[0] = score1;
        scores[1] = score2;

        grid = FindObjectOfType<HexGrid>();
        cellCells = FindObjectsOfType<HatrisHexCell>();
        
        foreach (var cell in cellCells)
        {
            HexCell parentHexCell = cell.transform.parent.parent.GetComponent<HexCell>();
            if (!cellList.Contains(parentHexCell))
            {
                cellList.Add(parentHexCell);
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

        playerCount++;
        playerCount = playerCount % 2;
    }

    public void FindHatMoves()
    {
        Debug.Log("Searching for hat moves");
        foreach (HexCell cell in cellList)
        {
            for (int i = 0; i < 6; i++)
            {
                HexCell neighbour1 = cell.GetNeighbor((HexDirection)((i + 4) % 6));
                HexCell neighbour2 = cell.GetNeighbor((HexDirection)((i + 5) % 6));
                if (neighbour1 != null && neighbour1.transform.GetChild(0).childCount == 6 && neighbour2 != null && neighbour2.transform.GetChild(0).childCount == 6)
                {
                    HatrisHexCell[] meshCells = new HatrisHexCell[8];
                    for (int j = 0; j < 4; j++)
                    {
                        meshCells[j] = cell.transform.GetChild(0).GetChild((i + ((j + 4) % 6)) % 6).GetComponent<HatrisHexCell>();
                    }
                    for (int j = 0; j < 2; j++)
                    {
                        meshCells[j + 4] = neighbour1.transform.GetChild(0).GetChild((i + ((j + 1) % 6)) % 6).GetComponent<HatrisHexCell>();
                        meshCells[j + 6] = neighbour2.transform.GetChild(0).GetChild((i + ((j + 3) % 6)) % 6).GetComponent<HatrisHexCell>();
                    }
                    int landPiecesCount = 0;
                    for (int k = 0; k < meshCells.Length; k++)
                    {
                        if (meshCells[k].hatPieceAbove != null)
                        {
                            landPiecesCount++;
                        }
                    }
                    if (landPiecesCount == 0)
                    {
                        FlashCells(cell);
                    }
                }
            }
        }
    }

    public void FindReverseHatMoves()
    {
        Debug.Log("Searching for reverse hat moves");
        foreach (HexCell cell in cellList)
        {
            for (int i = 0; i < 6; i++)
            {
                HexCell neighbour1 = cell.GetNeighbor((HexDirection)(i % 6));
                HexCell neighbour2 = cell.GetNeighbor((HexDirection)((i + 1) % 6));
                if (neighbour1 != null && neighbour1.transform.GetChild(0).childCount == 6 && neighbour2 != null && neighbour2.transform.GetChild(0).childCount == 6)
                {
                    HatrisHexCell[] meshCells = new HatrisHexCell[8];
                    for (int j = 0; j < 4; j++)
                    {
                        meshCells[j] = cell.transform.GetChild(0).GetChild((i + ((j + 5) % 6)) % 6).GetComponent<HatrisHexCell>();
                    }
                    for (int j = 0; j < 2; j++)
                    {
                        meshCells[j + 4] = neighbour1.transform.GetChild(0).GetChild((i % 6) % 6).GetComponent<HatrisHexCell>();
                        meshCells[j + 6] = neighbour2.transform.GetChild(0).GetChild((i + ((j + 1) % 6)) % 6).GetComponent<HatrisHexCell>();
                    }
                    int landPiecesCount = 0;
                    for (int k = 0; k < meshCells.Length; k++)
                    {
                        if (meshCells[k].hatPieceAbove != null)
                        {
                            landPiecesCount++;
                        }
                    }
                    if (landPiecesCount == 0)
                    {
                        FlashCells(cell);
                    }
                }
            }
        }
    }

    public void FlashCells(HexCell cell)
    {
       StartCoroutine(FlashCell(cell));
    }

    IEnumerator FlashCell(HexCell cell)
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 6; i++)
            {
                cell.transform.GetChild(0).GetChild(i).GetComponent<MeshRenderer>().material = potentialMat;
            }
            yield return new WaitForSeconds(1);
            for (int i = 0; i < 6; i++)
            {
                cell.transform.GetChild(0).GetChild(i).GetComponent<MeshRenderer>().material = origin;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
