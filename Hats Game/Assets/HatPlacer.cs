using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class HatPlacer : MonoBehaviour
{
    public Color[] colors;

    private HexGrid hexGrid;
    private Rigidbody rb;

    public bool isSelected;

    public int hatHits = 0;

    public HexCell currentCell;
    public HexCell landCell;

    public Vector3 thisHatRot;
    public int thisHatRotInt;

    [SerializeField] HexCoordinates cellCo;

    private GameManager gameManager;

    private ChecksValid validityCheck;

    void Start()
    {
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        landCell = hexGrid.GetCell(transform.position);
        currentCell = landCell;
        validityCheck = this.GetComponent<ChecksValid>();
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
        if (Input.GetMouseButtonDown(0))
            if (isSelected)
            {
                Deselect();
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
            else
            {
                this.transform.position = (stayCell.transform.position);
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
    private void OnMouseDown()
    {
        if (!gameManager.tileSelected)
        {
            Debug.Log("select");
            gameManager.tileSelected = true;
            HexCell landCell = hexGrid.GetCell(transform.position);
            landCell.hasHat = false;
            landCell.hasReverseHat = false;
            landCell.hatRot = 0;
            landCell.hatRotInt = 0;
            StartCoroutine(TrueSelecta());
            gameManager.selectedTile = this.gameObject; 
            Destroy(this.GetComponent<Rigidbody>());
        }
        else
        {
            Debug.Log("already selected");
        }

    }
    private void Deselect()
    {
        thisHatRot = transform.eulerAngles;
        thisHatRotInt = Mathf.RoundToInt(thisHatRot.y / 60) % 6;

        landCell = hexGrid.GetCell(transform.position);
        currentCell = landCell;
        if (!landCell.hasHat && !landCell.hasReverseHat)
        {
            validityCheck.IsPlacementValid(landCell);
            Debug.Log(validityCheck.isValid);
  
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
            isSelected = false;
                this.AddComponent<Rigidbody>();
                rb = GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
                gameManager.tileSelected = false;
                gameManager.selectedTile = null;
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
