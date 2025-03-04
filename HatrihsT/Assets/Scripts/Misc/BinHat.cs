using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinHat : MonoBehaviour
{
    private HexGrid hexGrid;
    private GameManager gameManager;
    public HexCell currentCell;
    public HexCell landCell;
    void Start()
    {
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        landCell = hexGrid.GetCell(transform.position);
        landCell.isBinHat = true;
    }
}
