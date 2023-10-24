using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class MultiHatPlacer : MonoBehaviour
{
    private HexGrid hexGrid;

    public bool isSelected;

    private ChecksValid[] validHats;
    private HexCell[] landCells;
    private HexCell[] currentCells;

    [SerializeField] HexCoordinates cellCo;

    private GameManager gameManager;

    private ChecksValid validityCheck;

    void Start()
    {
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        List<GameObject> hats = new List<GameObject>();
        List<ChecksValid> validHatsList = new List<ChecksValid>();

        foreach (Transform t in transform)
        {
            hats.Add(t.gameObject);
            if (t.gameObject.GetComponent<ChecksValid>())
            {
                t.gameObject.GetComponent<ChecksValid>().currentCell = hexGrid.GetCell(transform.position);
                validHatsList.Add(t.GetComponent<ChecksValid>());
            }
        }

        validHats = new ChecksValid[validHatsList.Count];
        landCells = new HexCell[validHatsList.Count];
        currentCells = new HexCell[validHatsList.Count];
        for (int i = 0; i < validHatsList.Count; i++)
        {
            validHats[i] = validHatsList[i];
            validHats[i].currentCell = hexGrid.GetCell(validHats[i].transform.position);
            landCells[i] = hexGrid.GetCell(validHatsList[i].transform.position);
            currentCells[i] = landCells[i];
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
            foreach (ChecksValid hat in validHats)
            {
                hat.thisHatRot = hat.transform.eulerAngles.y;
                hat.thisHatRotInt = Mathf.RoundToInt(hat.thisHatRot / 60) % 6;
            }
            
        }
    }
    private void OnMouseDown()
    {
        if (!gameManager.tileSelected)
        {
            Debug.Log("select");
            gameManager.tileSelected = true;
            foreach (ChecksValid hat in validHats)
            {
                HexCell landCell = hexGrid.GetCell(hat.transform.position);
                landCell.hasHat = false;
                landCell.hasReverseHat = false;
                landCell.hatRot = 0;
                landCell.hatRotInt = 0;
            }
            Destroy(this.GetComponent<Rigidbody>());
            StartCoroutine(TrueSelecta());
            gameManager.selectedTile = this.gameObject;
        }
        else
        {
            Debug.Log("already selected");
        }

    }
    private void Deselect()
    {
        Debug.Log("Deselect");
        int count = 0;
        for (int i = 0; i < validHats.Length; i++)
        {
            validHats[i].thisHatRot = validHats[i].transform.eulerAngles.y;
            validHats[i].thisHatRotInt = Mathf.RoundToInt(validHats[i].thisHatRot / 60) % 6;
            landCells[i] = hexGrid.GetCell(validHats[i].transform.position);
            currentCells[i] = landCells[i];
            
            if (!landCells[i].hasHat && !landCells[i].hasReverseHat)
            {
                count++;
            }
        }

        if (count == validHats.Length)
        {
            int count2 = 0;
            for (int i = 0; i < validHats.Length; i++)
            {
                validityCheck = validHats[i].GetComponent<ChecksValid>();
                validityCheck.IsPlacementValid(landCells[i]);
                if (validityCheck.isValid)
                {
                    count2++;
                    
                }
                else
                {
                    Debug.Log("hat " + (i + 1) + " is " + validityCheck.isValid);
                }
            }
            
  
            if (count2 == validHats.Length)
            {
                for (int i = 0; i < validHats.Length; i++)
                {
                    if (validHats[i].CompareTag("Hat"))
                    {
                        landCells[i].hasHat = true;
                    }
                    else if (validHats[i].CompareTag("Reverse Hat"))
                    {
                        landCells[i].hasReverseHat = true;
                    }

                    landCells[i].hatRot = Mathf.Round(validHats[i].transform.eulerAngles.y);
                    landCells[i].hatRotInt = Mathf.RoundToInt(landCells[i].hatRot / 60) % 6;
                }
                this.AddComponent<Rigidbody>();
                Rigidbody rb = this.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
                gameManager.tileSelected = false;
                gameManager.selectedTile = null;
                isSelected = false;
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
