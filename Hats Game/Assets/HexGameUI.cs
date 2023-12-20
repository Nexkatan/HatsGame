using UnityEngine;
using UnityEngine.EventSystems;

public class HexGameUI : MonoBehaviour
{

    public HexGrid grid;
    HexCell currentCell;
    public void SetEditMode()
    {
        enabled = !enabled;
        
    }

   
}