using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class HatPlacer : MonoBehaviour
{
    public Color[] colors;

    private GameManager gameManager;
    private HexGrid hexGrid;
    private Rigidbody rb;

    public bool isSelected;

    public HexCell currentCell;
    public HexCell landCell;

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
                
                validityCheck.IsPlacementValid(landCell);
                Debug.Log(validityCheck.isValid);

                if (gameManager.GetComponent<TilingHoleMaker>())
                {
                    if (transform.position.x > 700 || transform.position.x < 150 || transform.position.z > 470 || transform.position.z < 150)
                    {
                        Debug.Log("Out of Bounds");
                        validityCheck.isValid = false;
                    }
                }

                if (validityCheck.isValid)
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
                    
                    for (int i = 0; i < buttons.Count; i++)
                    {
                        buttons[i].interactable = true;
                    }

                    if (gameManager.GetComponent<ChainManager>())
                    {
                        gameManager.GetComponent<ChainManager>().AddHatsToList();
                    }
                    if (gameManager.GetComponent<TilingHoleMaker>())
                    {
                        if (this.CompareTag("Hat"))
                        {
                            gameManager.GetComponent<TilingHoleMaker>().hats -= 1;
                        }
                        else if (this.CompareTag("Reverse Hat"))
                        {
                            gameManager.GetComponent<TilingHoleMaker>().reverseHats -= 1;
                        }
                        if (gameManager.GetComponent<TilingHoleMaker>().hats + gameManager.GetComponent<TilingHoleMaker>().reverseHats == 0)
                        {
                            gameManager.GetComponent<TilingHoleMaker>().LevelComplete();
                        }
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
