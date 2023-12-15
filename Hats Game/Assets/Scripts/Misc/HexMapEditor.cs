using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour
{
    public Color[] colors;

    public HexGrid hexGrid;

    private Color activeColor;

    private HexCell previousCell;

    void Awake()
    {
        SelectColor(0);
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject()
        )
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        
        HexCell currentCell = GetCellUnderCursor();
        if (currentCell)
        {
            //Debug.Log(currentCell.coordinates);
		}
        else
        {
            previousCell = null;
        }

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(inputRay, out hit))
            {
                hexGrid.ColorCell(hit.point, activeColor);
            }

        }

    }

    HexCell GetCellUnderCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 200;
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
       
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            return hexGrid.GetCell(hit.point);
        }
        return null;
    }



    public void SelectColor(int index)
    {
        activeColor = colors[index];
    }
}
