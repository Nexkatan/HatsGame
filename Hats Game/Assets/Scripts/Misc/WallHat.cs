using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallHat : MonoBehaviour
{
    public HexCell moveCell;
    public HexCell currentCell;
    private GameManager gameManager;
    private HexGrid hexGrid;

    public bool moveTime;
    public bool alternate;

    private bool gameOver = false;

    public bool isPlayer = false;

    public ChecksValid validityCheck;

    private void Start()
    {
        validityCheck = this.GetComponent<ChecksValid>();
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
      
        currentCell = hexGrid.GetCell(transform.position);
        SetHex();
        currentCell.hatRot = Mathf.Round(transform.eulerAngles.y);
    }

    private void Update()
    {
        if (hexGrid.GetCell(transform.position).coordinates.Z < 2)
        {
            Destroy(this.gameObject);
            ResetHex(currentCell);
        }
    }

    public void SetHex()
    {
        if (this.CompareTag("Hat"))
        {
            currentCell.hasHat = true;
        }
        else if (this.CompareTag("Reverse Hat"))
        {
            currentCell.hasReverseHat = true;
        }
        currentCell.hatRot = Mathf.Round(transform.eulerAngles.y);
        currentCell.hatRotInt = Mathf.RoundToInt(currentCell.hatRot / 60) % 6;
        currentCell.hatAbove = this.gameObject;
    }

    public void ResetHex(HexCell currentCell)
    {
        if (this.CompareTag("Hat"))
        {
            currentCell.hasHat = false;
        }
        else if (this.CompareTag("Reverse Hat"))
        {
            currentCell.hasReverseHat = false;
        }
        currentCell.hatRot = 0;
        currentCell.hatRotInt = 0;
        currentCell.hatAbove = null;
    }
   
}
