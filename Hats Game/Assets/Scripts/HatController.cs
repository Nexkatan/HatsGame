using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class HatController : MonoBehaviour
{
    public Color[] colors;

    private HexGrid hexGrid;
    private Rigidbody rb;

    private Vector3 mousePos;
    private Camera screenCamera;

    public GameObject colliders;

    public bool isSelected;

    public int hatHits = 0;

    public HexCell landCell;
    [SerializeField] HexCoordinates cellCo;

    private bool isValid;

    void Start()
    {
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        screenCamera = Camera.main;
    }

    void FixedUpdate()
    {
        if (isSelected)
        { 
            Move();
        }
        else
        {

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

    void Move()
    {
            mousePos = Input.mousePosition;

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
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("select");
        HexCell landCell = hexGrid.GetCell(transform.position);
        landCell.hasHat = false;
        landCell.hatRot = 0;
        StartCoroutine(TrueSelecta());
        Destroy(this.GetComponent<Rigidbody>());
    }

    private void Deselect()
    {
        landCell = hexGrid.GetCell(transform.position);
        if (!landCell.hasHat)
        {
            cellCo = landCell.coordinates;

            isPlacementValid();

            if (isValid)
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
                isSelected = false;
                this.AddComponent<Rigidbody>();
                rb = GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
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

    void isPlacementValid()
    {
        HexCell neighborNE = landCell.GetNeighbor(HexDirection.NE);
        HexCell neighborE = landCell.GetNeighbor(HexDirection.E);
        HexCell neighborSE = landCell.GetNeighbor(HexDirection.SE);
        HexCell neighborSW = landCell.GetNeighbor(HexDirection.SW);
        HexCell neighborW = landCell.GetNeighbor(HexDirection.W);
        HexCell neighborNW = landCell.GetNeighbor(HexDirection.NW);

        
        if (Mathf.Round(transform.eulerAngles.y) == 0)
        {
            if (neighborSW.hasHat)
            {
                if (neighborSW.hatRot == 0 || neighborSW.hatRot == 120 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240 || neighborSW.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborW.hasHat)
            {
                Debug.Log("E Hat");
                if (neighborW.hatRot == 120 || neighborW.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborNW.hasHat)
            {
                Debug.Log("SE hat");
                if (neighborNW.hatRot == 60)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborNE.hasHat)
            {
                Debug.Log("SW Hat");
                if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 180)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborE.hasHat)
            {
                Debug.Log("W Hat");
                if (neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 180 || neighborE.hatRot == 240 || neighborE.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborSE.hasHat)
            {
                if (neighborSE.hatRot == 60 || neighborSE.hatRot == 120 || neighborSE.hatRot == 180 || neighborSE.hatRot == 240 || neighborSE.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = true;
            }
        }

        else if (Mathf.Round(transform.eulerAngles.y) == 60)
        {
            if (neighborW.hasHat)
            {
                if (neighborW.hatRot == 0 || neighborW.hatRot == 60 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborNW.hasHat)
            {
                Debug.Log("E Hat");
                if (neighborNW.hatRot == 0)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborNE.hasHat)
            {
                Debug.Log("SE hat");
                if (neighborNE.hatRot == 120)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborE.hasHat)
            {
                Debug.Log("SW Hat");
                if (neighborE.hatRot == 60 || neighborE.hatRot == 180 || neighborE.hatRot == 240)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborSE.hasHat)
            {
                Debug.Log("W Hat");
                if (neighborSE.hatRot == 0 || neighborSE.hatRot == 120 || neighborSE.hatRot == 180 || neighborSE.hatRot == 240 || neighborSE.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborSW.hasHat)
            {
                if (neighborSW.hatRot == 0 || neighborSW.hatRot == 120 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240 || neighborSW.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = true;
            }
        }

        else if (Mathf.Round(transform.eulerAngles.y) == 120)
        {
            if (neighborNW.hasHat)
            {
                if (neighborNW.hatRot == 0 || neighborNW.hatRot == 60 || neighborNW.hatRot == 120 || neighborNW.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborNE.hasHat)
            {
                Debug.Log("E Hat");
                if (neighborNE.hatRot == 60)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborE.hasHat)
            {
                Debug.Log("SE hat");
                if (neighborE.hatRot == 180)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborSE.hasHat)
            {
                Debug.Log("SW Hat");
                if (neighborSE.hatRot == 180 || neighborSE.hatRot == 120 || neighborSE.hatRot == 240 || neighborSE.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborSW.hasHat)
            {
                Debug.Log("W Hat");
                if (neighborSW.hatRot == 0 || neighborSW.hatRot == 60 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240 || neighborSW.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborW.hasHat)
            {
                if (neighborW.hatRot == 0 || neighborW.hatRot == 60 || neighborW.hatRot == 180 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = true;
            }
        }

        else if (Mathf.Round(transform.eulerAngles.y) == 180)
        {
            if (neighborNE.hasHat)
            {
                if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 120 || neighborNE.hatRot == 180)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborE.hasHat)
            {
                Debug.Log("E Hat");
                if (neighborE.hatRot == 120)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborSE.hasHat)
            {
                Debug.Log("SE hat");
                if (neighborSE.hatRot == 240)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborSW.hasHat)
            {
                Debug.Log("SW Hat");
                if (neighborSW.hatRot == 0 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240 || neighborSW.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborW.hasHat)
            {
                Debug.Log("W Hat");
                if (neighborW.hatRot == 0 || neighborW.hatRot == 60 || neighborW.hatRot == 120 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborNW.hasHat)
            {
                if (neighborNW.hatRot == 0 || neighborNW.hatRot == 60 || neighborNW.hatRot == 120 || neighborNW.hatRot == 240 || neighborNW.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = true;
            }
        }

        else if (Mathf.Round(transform.eulerAngles.y) == 240)
        {
            if (neighborE.hasHat)
            {
                if (neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 180 || neighborE.hatRot == 240)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborSE.hasHat)
            {
                Debug.Log("E Hat");
                if (neighborSE.hatRot == 180)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborSW.hasHat)
            {
                Debug.Log("SE hat");
                if (neighborSW.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborW.hasHat)
            {
                Debug.Log("SW Hat");
                if (neighborW.hatRot == 0 || neighborW.hatRot == 60 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborNW.hasHat)
            {
                Debug.Log("W Hat");
                if (neighborNW.hatRot == 0 || neighborNW.hatRot == 60 || neighborNW.hatRot == 120 || neighborNW.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborNE.hasHat)
            {
                if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 120 || neighborNE.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = true;
            }
        }

        else if (Mathf.Round(transform.eulerAngles.y) == 300)
        {
            if (neighborSE.hasHat)
            {
                if (neighborSE.hatRot == 120 || neighborSE.hatRot == 180 || neighborSE.hatRot == 240 || neighborSE.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborSW.hasHat)
            {
                Debug.Log("E Hat");
                if (neighborSW.hatRot == 240)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborW.hasHat)
            {
                Debug.Log("SE hat");
                if (neighborW.hatRot == 0)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborNW.hasHat)
            {
                Debug.Log("SW Hat");
                if (neighborNW.hatRot == 0 || neighborNW.hatRot == 60 || neighborNW.hatRot == 120 || neighborNW.hatRot == 300)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborNE.hasHat)
            {
                Debug.Log("W Hat");
                if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 120 || neighborNE.hatRot == 180 || neighborNE.hatRot == 240)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else if (neighborE.hasHat)
            {
                if (neighborE.hatRot == 0 || neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 180 || neighborE.hatRot == 240)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = true;
            }
        }


        else
        {
            isValid = true;
        }
    }

    IEnumerator TrueSelecta()
    {
        yield return new WaitForSeconds(0.05f);
        isSelected = true;
    }
}
