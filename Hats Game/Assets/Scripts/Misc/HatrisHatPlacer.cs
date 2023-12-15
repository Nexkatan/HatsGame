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
    public List<Button> buttons = new List<Button>();

    private ChecksValid validityCheck;

    void Start()
    {
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        landCell = hexGrid.GetCell(transform.position);
        currentCell = landCell;
        validityCheck = this.GetComponent<ChecksValid>();
        HatTab = GameObject.Find("HatTab");
        if (HatTab)
        {
            foreach (Button button in HatTab.transform.GetChild(0).GetChild(1).GetComponentsInChildren<Button>())
            {
                buttons.Add(button);
            }
        }
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
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].interactable = true;
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
                        for (int i = 0; i < 6; i++)
                        {
                            Destroy(landCell.transform.GetChild(0).GetChild(i).GetComponent<HatrisHexCell>().hatPieceAbove);
                            landCell.transform.GetChild(0).GetChild(i).GetComponent<HatrisHexCell>().hatPieceAbove = null;
                            landCell.hasHat = false;
                            landCell.hasReverseHat = false;
                        }
                    }
                    if (count3 == 6)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            Destroy(neighbour1.transform.GetChild(0).GetChild(i).GetComponent<HatrisHexCell>().hatPieceAbove);
                            neighbour1.transform.GetChild(0).GetChild(i).GetComponent<HatrisHexCell>().hatPieceAbove = null;
                            neighbour1.hasHat = false;
                            neighbour1.hasReverseHat = false;
                        }
                    }
                    if (count4 == 6)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            Destroy(neighbour2.transform.GetChild(0).GetChild(i).GetComponent<HatrisHexCell>().hatPieceAbove);
                            neighbour2.transform.GetChild(0).GetChild(i).GetComponent<HatrisHexCell>().hatPieceAbove = null;
                            neighbour2.hasHat = false;
                            neighbour2.hasReverseHat = false;
                        }
                    }

                    for (int i = 0; i < buttons.Count; i++)
                    {
                        buttons[i].interactable = true;
                    }
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
    IEnumerator TrueSelecta()
    {
        yield return new WaitForSeconds(0.05f);
        isSelected = true;
    }
}
