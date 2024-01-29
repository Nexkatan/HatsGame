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

    public TextMeshProUGUI winner;

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
    public List<HexCell> possibleMoves;

    public List<HexCell> onePtHatMoves;
    public List<HexCell> twoPtHatMoves;
    public List<HexCell> threePtHatMoves;

    public List<HexCell> onePtReverseHatMoves;
    public List<HexCell> twoPtReverseHatMoves;
    public List<HexCell> threePtReverseHatMoves;

    public List<HexCell> onePtMoves;
    public List<HexCell> twoPtMoves;
    public List<HexCell> threePtMoves;

    public bool cellFlash;
    public HexCell AIMove;
    public bool AIMoveIsReverse;
    public int AIMoveIntRotation;
    public GameObject AIHat;

    public bool scoreCell;
    public bool scoreNeighbour1;
    public bool scoreNeighbour2;

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
        ClearHatsLists();
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
                        possibleMoves.Add(cell);
                        if (cellFlash)
                        {
                            FlashCells(cell);
                        }


                        int count3 = 0;
                        if (cell.transform.GetChild(0).GetChild((i + 0) % 6).GetComponent<HatrisHexCell>().hatPieceAbove == null && cell.transform.GetChild(0).GetChild((i + 1) % 6).GetComponent<HatrisHexCell>().hatPieceAbove == null && cell.transform.GetChild(0).GetChild((i + 2) % 6).GetComponent<HatrisHexCell>().hatPieceAbove != null && cell.transform.GetChild(0).GetChild((i + 3) % 6).GetComponent<HatrisHexCell>().hatPieceAbove != null && cell.transform.GetChild(0).GetChild((i + 4) % 6).GetComponent<HatrisHexCell>().hatPieceAbove == null && cell.transform.GetChild(0).GetChild((i + 5) % 6).GetComponent<HatrisHexCell>().hatPieceAbove == null)
                        {
                            count3++;
                            cell.isReverseHatMove = false;
                            cell.moveIntRotation = i;
                        }

                        int count1 = 0;
                        int count2 = 0;
                        for (int k = 0; k < 4; k++)
                        {
                            if (neighbour1.transform.GetChild(0).GetChild((i + 3 + k) % 6).GetComponent<HatrisHexCell>().hatPieceAbove != null)
                            {
                                count1++;
                            }
                            if (neighbour2.transform.GetChild(0).GetChild((i + 5 + k) % 6).GetComponent<HatrisHexCell>().hatPieceAbove != null)
                            {
                                count2++;
                            }
                        }
                        if (count1 == 4)
                        {
                            count3++;
                        }
                        if (count2 == 4)
                        {
                            count3++;
                        }

                        if (count3 == 3)
                        {
                            threePtHatMoves.Add(cell);
                        }
                        if (count3 == 2)
                        {
                            twoPtHatMoves.Add(cell);
                            cell.moveIntRotation = i;
                        }
                        if (count3 == 1)
                        {
                            onePtHatMoves.Add(cell);
                            cell.moveIntRotation = i;
                        }

                    }
                }
            }
        }
    }

    public void FindReverseHatMoves()
    {
        Debug.Log("Searching for reverse hat moves");
        ClearReverseHatsLists();
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
                        meshCells[j + 4] = neighbour1.transform.GetChild(0).GetChild((i + ((j + 2) % 6)) % 6).GetComponent<HatrisHexCell>();
                        meshCells[j + 6] = neighbour2.transform.GetChild(0).GetChild((i + ((j + 4) % 6)) % 6).GetComponent<HatrisHexCell>();
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
                        possibleMoves.Add(cell);
                        if (cellFlash)
                        {
                            FlashCells(cell);
                        }


                        int count3 = 0;
                        if (cell.transform.GetChild(0).GetChild((i + 0) % 6).GetComponent<HatrisHexCell>().hatPieceAbove == null && cell.transform.GetChild(0).GetChild((i + 1) % 6).GetComponent<HatrisHexCell>().hatPieceAbove == null && cell.transform.GetChild(0).GetChild((i + 2) % 6).GetComponent<HatrisHexCell>().hatPieceAbove == null && cell.transform.GetChild(0).GetChild((i + 3) % 6).GetComponent<HatrisHexCell>().hatPieceAbove != null && cell.transform.GetChild(0).GetChild((i + 4) % 6).GetComponent<HatrisHexCell>().hatPieceAbove != null && cell.transform.GetChild(0).GetChild((i + 5) % 6).GetComponent<HatrisHexCell>().hatPieceAbove == null)
                        {
                            count3++;
                            cell.isReverseHatMove = true;
                            Debug.Log(cell + ": i: " + i);
                            cell.moveIntRotation = i;
                        }

                        int count1 = 0;
                        int count2 = 0;
                        for (int k = 0; k < 4; k++)
                        {
                            if (neighbour1.transform.GetChild(0).GetChild((i + 4 + k) % 6).GetComponent<HatrisHexCell>().hatPieceAbove != null)
                            {
                                count1++;
                            }
                            if (neighbour2.transform.GetChild(0).GetChild((i + k) % 6).GetComponent<HatrisHexCell>().hatPieceAbove != null)
                            {
                                count2++;
                            }
                        }
                        if (count1 == 4)
                        {
                            count3++;
                            Debug.Log(cell + ": i: " + i);
                            cell.moveIntRotation = i;
                        }
                        if (count2 == 4)
                        {
                            count3++;
                            Debug.Log(cell + ": i: " + i);
                            cell.moveIntRotation = i;
                        }

                        if (count3 == 3)
                        {
                            threePtReverseHatMoves.Add(cell);
                        }
                        if (count3 == 2)
                        {
                            twoPtReverseHatMoves.Add(cell);
                        }
                        if (count3 == 1)
                        {
                            onePtReverseHatMoves.Add(cell);
                        }

                        
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
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 6; i++)
            {
                cell.transform.GetChild(0).GetChild(i).GetComponent<MeshRenderer>().material = potentialMat;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 6; i++)
            {
                cell.transform.GetChild(0).GetChild(i).GetComponent<MeshRenderer>().material = origin;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void CheckGameOver()
    {
        FindHatMoves();
        if (possibleMoves.Count == 0)
        {
            FindReverseHatMoves();
            if (possibleMoves.Count == 0)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        GameObject HatTab = GameObject.FindGameObjectWithTag("Hat Tab");
        HatTab.SetActive(false);
        winner.gameObject.SetActive(true);
        if (score1 > score2)
        {
            winner.text = "Player 1 wins!";
        }
        else if (score1 < score2)
        {
            winner.text = "Player 2 wins!";
        }
        else
        {
            winner.text = "It's a draw";
        }
    }

    public void cellFlashOn()
    {
        StartCoroutine(flashBool());
    }

    IEnumerator flashBool()
    {
        cellFlash = true;
        yield return new WaitForSeconds(1);
        cellFlash = false;
    }

    void ClearHatsLists()
    {
        possibleMoves.Clear();
        onePtHatMoves.Clear();
        twoPtHatMoves.Clear();
        threePtHatMoves.Clear();

        scoreCell = false;
        scoreNeighbour1 = false;
        scoreNeighbour2 = false;
    }

    void ClearReverseHatsLists()
    {
        possibleMoves.Clear();
        onePtReverseHatMoves.Clear();
        twoPtReverseHatMoves.Clear();
        threePtReverseHatMoves.Clear();


        scoreCell = false;
        scoreNeighbour1 = false;
        scoreNeighbour2 = false;
    }


    public void MoveAIPlayerHat()
    {
        ClearHatsLists();
        ClearReverseHatsLists();

        onePtMoves.Clear();
        twoPtMoves.Clear();
        threePtMoves.Clear();

        FindHatMoves();
        //FindReverseHatMoves();
        AddMovesToList();

        if (threePtMoves.Count > 0)
        {
            ChooseRandomMoveFromList(threePtMoves);
        }
        else if (twoPtMoves.Count > 0)
        {
            ChooseRandomMoveFromList(twoPtMoves);
        }
        else if (onePtMoves.Count > 0)
        {
            ChooseRandomMoveFromList(onePtMoves);
        }

        MoveThere(AIMove);
    }

    void AddMovesToList()
    {
        if (possibleMoves.Count > 0)
        {
            if (threePtHatMoves.Count > 0 || threePtReverseHatMoves.Count > 0)
            {
                if (threePtHatMoves.Count >= threePtReverseHatMoves.Count)
                {
                    AIMoveIsReverse = false;
                    for (int i = 0; i < threePtHatMoves.Count; i++)
                    {
                        threePtMoves.Add(threePtHatMoves[i]);
                    }
                }
                else if (threePtHatMoves.Count < threePtReverseHatMoves.Count)
                {
                    AIMoveIsReverse = true;
                    for (int i = 0; i < threePtReverseHatMoves.Count; i++)
                    {
                        threePtMoves.Add(threePtReverseHatMoves[i]);
                    }
                }
            }
            if (twoPtHatMoves.Count > 0 || twoPtReverseHatMoves.Count > 0)
            {
                if (twoPtHatMoves.Count >= twoPtReverseHatMoves.Count)
                {
                    AIMoveIsReverse = false;
                    for (int i = 0; i < twoPtHatMoves.Count; i++)
                    {
                        twoPtMoves.Add(twoPtHatMoves[i]);
                    }
                }
                else if (twoPtHatMoves.Count < twoPtReverseHatMoves.Count)
                {
                    AIMoveIsReverse = true;
                    for (int i = 0; i < twoPtReverseHatMoves.Count; i++)
                    {
                        twoPtMoves.Add(twoPtReverseHatMoves[i]);
                    }
                }
            }
            if (onePtHatMoves.Count > 0 || onePtReverseHatMoves.Count > 0)
            {
                if (onePtHatMoves.Count >= onePtReverseHatMoves.Count)
                {
                    AIMoveIsReverse = false;
                    for (int i = 0; i < onePtHatMoves.Count; i++)
                    {
                        onePtMoves.Add(onePtHatMoves[i]);
                    }
                }
                else if (onePtHatMoves.Count < onePtReverseHatMoves.Count)
                {
                    AIMoveIsReverse = true;
                    for (int i = 0; i < onePtReverseHatMoves.Count; i++)
                    {
                        onePtMoves.Add(onePtReverseHatMoves[i]);
                    }
                }
            }

            else
            {
                HexCell cellFlash = possibleMoves[Random.Range(0, possibleMoves.Count)];
                Debug.Log("0pts at: " + cellFlash);
                FlashCells(cellFlash);
            }
        }
    }

    void ChooseRandomMoveFromList(List<HexCell> list)
    {
        HexCell cellFlash = list[Random.Range(0, list.Count)];
        FlashCells(cellFlash);
        AIMove = cellFlash;
       
        AIMoveIntRotation = cellFlash.moveIntRotation;
        Debug.Log(cellFlash);
    }

    void MoveThere(HexCell AImoveCell)
    {
        HatrisHatPlacer AIHatToMove = Instantiate(AIHat, AImoveCell.transform.position + new Vector3(0, 5, 0), AIHat.transform.rotation).GetComponent<HatrisHatPlacer>();
        if (AIMoveIsReverse)
        {
            Debug.Log("ReverseMove");
            AIHatToMove.transform.localScale = new Vector3(-AIHatToMove.transform.localScale.x, AIHatToMove.transform.localScale.y, AIHatToMove.transform.localScale.z);
        }
        
        RotateAIHat(AIHatToMove);

    }

    void RotateAIHat(HatrisHatPlacer AIHat)
    {
        Vector3 m_EulerAngleVelocity = new Vector3(0, 60 * AIMoveIntRotation, 0);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity);
        AIHat.transform.rotation *= deltaRotation;
        AIHat.thisHatRot = transform.eulerAngles;
        AIHat.thisHatRotInt = Mathf.RoundToInt(AIHat.thisHatRot.y / 60) % 6;
    }
}
