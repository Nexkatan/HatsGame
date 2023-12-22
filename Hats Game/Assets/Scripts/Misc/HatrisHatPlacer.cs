using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class HatrisHatPlacer : MonoBehaviour
{
    public Color[] colors;

    private GameManager gameManager;
    private HexGrid hexGrid;
    private Rigidbody rb;

    public bool isSelected;

    public HexCell currentCell;
    public HexCell landCell;

    public HexCell neighbour1;
    public HexCell neighbour2;

    public Vector3 thisHatRot;
    public int thisHatRotInt;

    [SerializeField] HexCoordinates cellCo;

    GameObject HatTab;
    public List<Button> player1buttons = new List<Button>();
    public List<Button> player2buttons = new List<Button>();

    private ChecksValid validityCheck;


    public enum Team
    {
        None,
        Pink,
        Purple
    }

    public Team team;

    private Material teamMat;
    public Material defaultMat;

    private int playerCount;

    public HatrisScoreKeeper scoreKeeper;

    void Start()
    {
        hexGrid = GameObject.FindObjectOfType<HexGrid>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        landCell = hexGrid.GetCell(transform.position);
        currentCell = landCell;
        validityCheck = this.GetComponent<ChecksValid>();
        HatTab = GameObject.Find("HatTab");
        if (HatTab)
        {
            foreach (Button button in HatTab.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponentsInChildren<Button>())
            {
                player1buttons.Add(button);
            }
            foreach (Button button in HatTab.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetComponentsInChildren<Button>())
            {
                player2buttons.Add(button);
            }
        }

        teamMat = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material;

        scoreKeeper = GameObject.Find("GameManager").GetComponent<HatrisScoreKeeper>();
    }

    void FixedUpdate()
    {
        if (isSelected)
        {
            MouseMove();
        }
    }
    private void Update()
    {
        if (isSelected)
        {
            Spin();
        }
        if (isSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Deselect();
            }
        }
        DeleteHat();
    }

    void MouseMove()
    {
        if (GetCellUnderCursor() != null)
        {
            HexCell currentCell = GetCellUnderCursor();
            HexCell stayCell = hexGrid.GetCell(transform.position);
            if (isSelected)
            {
                this.transform.position = (currentCell.transform.position);
            }
            if (this.CompareTag("Hat"))
            {
                neighbour1 = currentCell.GetNeighbor((HexDirection)((thisHatRotInt + 4) % 6));
                neighbour2 = currentCell.GetNeighbor((HexDirection)((thisHatRotInt + 5) % 6));

                if (currentCell.isHatrisCell && neighbour1.isHatrisCell && neighbour2.isHatrisCell)
                {
                    transform.position = new Vector3(transform.position.x, 5f, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, -1f, transform.position.z);
                }
            }
            else if (this.CompareTag("Reverse Hat"))
            {
                neighbour1 = currentCell.GetNeighbor((HexDirection)((thisHatRotInt) % 6));
                neighbour2 = currentCell.GetNeighbor((HexDirection)((thisHatRotInt + 1) % 6));
               
                if (currentCell.isHatrisCell && neighbour1.isHatrisCell && neighbour2.isHatrisCell)
                {
                    transform.position = new Vector3(transform.position.x, 5f, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, -1f, transform.position.z);
                }
            }
        }
    }
    void Spin()
    {
        if (Input.GetMouseButtonDown((1)))
        {
            Vector3 m_EulerAngleVelocity = new Vector3(0, 60, 0);
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity);
            this.transform.rotation *= deltaRotation;
            thisHatRot = transform.eulerAngles;
            thisHatRotInt = Mathf.RoundToInt(thisHatRot.y / 60) % 6;
        }
    }
    
    private void Deselect()
    {
        Debug.Log("Deselect");
        thisHatRot = transform.eulerAngles;
        thisHatRotInt = Mathf.RoundToInt(thisHatRot.y / 60) % 6;

        landCell = hexGrid.GetCell(transform.position);
        currentCell = landCell;



        if (landCell.isBinHat)
        {
            gameManager.tileSelected = false;
            gameManager.selectedTile = null;
            Destroy(gameObject);
            for (int i = 0; i < player1buttons.Count; i++)
            {
                player1buttons[i].interactable = true;
                player2buttons[i].interactable = true;
            }
        }
        else
        {
            if (!landCell.hasHat && !landCell.hasReverseHat)
            {
                HatrisHexCell[] meshCells = new HatrisHexCell[8];

                int count = 0;

                if (CompareTag("Hat"))
                {
                    meshCells[0] = landCell.transform.GetChild(0).GetChild(thisHatRotInt).GetComponent<HatrisHexCell>();
                    meshCells[1] = landCell.transform.GetChild(0).GetChild((thisHatRotInt + 1) % 6).GetComponent<HatrisHexCell>();
                    meshCells[2] = landCell.transform.GetChild(0).GetChild((thisHatRotInt + 4) % 6).GetComponent<HatrisHexCell>();
                    meshCells[3] = landCell.transform.GetChild(0).GetChild((thisHatRotInt + 5) % 6).GetComponent<HatrisHexCell>();

                    neighbour1 = landCell.GetNeighbor((HexDirection)((thisHatRotInt + 4) % 6));
                    neighbour2 = landCell.GetNeighbor((HexDirection)((thisHatRotInt + 5) % 6));

                    meshCells[4] = neighbour1.transform.GetChild(0).GetChild((thisHatRotInt + 1) % 6).GetComponent<HatrisHexCell>();
                    meshCells[5] = neighbour1.transform.GetChild(0).GetChild((thisHatRotInt + 2) % 6).GetComponent<HatrisHexCell>();

                    meshCells[6] = neighbour2.transform.GetChild(0).GetChild((thisHatRotInt + 3) % 6).GetComponent<HatrisHexCell>();
                    meshCells[7] = neighbour2.transform.GetChild(0).GetChild((thisHatRotInt + 4) % 6).GetComponent<HatrisHexCell>();

                    for (int i = 0; i < meshCells.Length; i++)
                    {
                        if (meshCells[i].hatPieceAbove != null)
                        {
                            count++;
                        }
                    }
                }
                else if (CompareTag("Reverse Hat"))
                {
                    meshCells[0] = landCell.transform.GetChild(0).GetChild((thisHatRotInt + 5) % 6).GetComponent<HatrisHexCell>();
                    meshCells[1] = landCell.transform.GetChild(0).GetChild((thisHatRotInt + 0) % 6).GetComponent<HatrisHexCell>();
                    meshCells[2] = landCell.transform.GetChild(0).GetChild((thisHatRotInt + 1) % 6).GetComponent<HatrisHexCell>();
                    meshCells[3] = landCell.transform.GetChild(0).GetChild((thisHatRotInt + 2) % 6).GetComponent<HatrisHexCell>();

                    neighbour1 = landCell.GetNeighbor((HexDirection)((thisHatRotInt) % 6));
                    neighbour2 = landCell.GetNeighbor((HexDirection)((thisHatRotInt + 1) % 6));

                    Debug.Log((HexDirection)((thisHatRotInt) % 6));
                    Debug.Log((HexDirection)((thisHatRotInt + 1) % 6));

                    meshCells[4] = neighbour2.transform.GetChild(0).GetChild((thisHatRotInt + 4) % 6).GetComponent<HatrisHexCell>();
                    meshCells[5] = neighbour2.transform.GetChild(0).GetChild((thisHatRotInt + 5) % 6).GetComponent<HatrisHexCell>();

                    meshCells[6] = neighbour1.transform.GetChild(0).GetChild((thisHatRotInt + 2) % 6).GetComponent<HatrisHexCell>();
                    meshCells[7] = neighbour1.transform.GetChild(0).GetChild((thisHatRotInt + 3) % 6).GetComponent<HatrisHexCell>();

                    for (int i = 0; i < meshCells.Length; i++)
                    {
                        if (meshCells[i].hatPieceAbove != null)
                        {
                            count++;
                        }
                    }
                }


                if (count == 0)
                {
                    if (this.CompareTag("Hat"))
                    {
                        landCell.hasHat = true;
                    }
                    else if (this.CompareTag("Reverse Hat"))
                    {
                        landCell.hasReverseHat = true;
                    }

                    landCell.hatRot = Mathf.Round(transform.eulerAngles.y);
                    landCell.hatRotInt = Mathf.RoundToInt(landCell.hatRot / 60) % 6;
                    landCell.hatAbove = this.gameObject;
                    isSelected = false;
                    gameManager.tileSelected = false;
                    gameManager.selectedTile = null;

                    GameObject[] hatPieces = new GameObject[8];

                    for (int i = 0; i < 8; i++)
                    {
                        hatPieces[i] = transform.GetChild(0).GetChild(0).GetChild(i).gameObject;
                        hatPieces[i].name = "hatPiece " + i;
                        meshCells[i].hatPieceAbove = hatPieces[i];
                        //meshCells[i].GetComponent<MeshRenderer>().material = teamMat;
                    }

                    

                    int count2 = 0;
                    int count3 = 0;
                    int count4 = 0;

                    for (int i = 0; i < 6; i++)
                    {
                        if (landCell.transform.GetChild(0).GetChild(i).GetComponent<HatrisHexCell>().hatPieceAbove != null)
                        {
                            count2++;
                        }
                        if (neighbour1.transform.GetChild(0).GetChild(i).GetComponent<HatrisHexCell>().hatPieceAbove != null)
                        {
                            count3++;
                        }
                        if (neighbour2.transform.GetChild(0).GetChild(i).GetComponent<HatrisHexCell>().hatPieceAbove != null)
                        {
                            count4++;
                        }
                    }
                    
                    if (count2 == 6)
                    {
                        Score(landCell);
                    }
                    if (count3 == 6)
                    {
                        Score(neighbour1);
                    }
                    if (count4 == 6)
                    {
                        Score(neighbour2);
                    }

                    int extraScore = 0;

                    int[] counts = new int[3];

                    counts[0] = count2;
                    counts[1] = count3;
                    counts[2] = count4;

                    for (int i = 0; i < counts.Length; i++)
                    {
                        if (counts[i] % 6 == 0)
                        {
                            extraScore++;
                        }
                    }

                    if (extraScore > 0)
                    {
                        //scoreKeeper.AddScore(extraScore * 2 - 1);
                        scoreKeeper.KeepScore();
                    }

                    scoreKeeper.playerCount++;
                    scoreKeeper.playerCount = scoreKeeper.playerCount % 2;

                    ResetButton();
                }
                else
                {
                    Debug.Log("Invalid");
                }
            }
            else
            {
                Debug.Log("Hat Already here");
            }
        }
    }
    HexCell GetCellUnderCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 200;
        Ray inputRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            return hexGrid.GetCell(hit.point);
        }
        return null;
    }

    void Score(HexCell cell)
    {
        for (int i = 0; i < 6; i++)
        {
            Destroy(cell.transform.GetChild(0).GetChild(i).GetComponent<HatrisHexCell>().hatPieceAbove);
            cell.transform.GetChild(0).GetChild(i).gameObject.GetComponent<MeshRenderer>().material = teamMat;
            int teamToScore = 
            cell.playerCellScored = (int)team;
            cell.transform.GetChild(0).GetChild(i).GetComponent<HatrisHexCell>().hatPieceAbove = null;
            cell.hasHat = false;
            cell.hasReverseHat = false;
        }
    }

    void ResetButton()
    {
        if (scoreKeeper.playerCount == 0)
        {
            for (int i = 0; i < player1buttons.Count; i++)
            {
                player1buttons[i].interactable = true;
                player2buttons[i].interactable = false;
            }
        }
        else
        {
            for (int i = 0; i < player1buttons.Count; i++)
            {
                player1buttons[i].interactable = false;
                player2buttons[i].interactable = true;
            }
        }
    }

    void DeleteHat()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameManager.tileSelected)
            {
                Destroy(gameManager.selectedTile.gameObject);
                gameManager.tileSelected = false;
            }
            ResetButton();
        }
    }

    IEnumerator TrueSelecta()
    {
        yield return new WaitForSeconds(0.05f);
        isSelected = true;
    }


}
