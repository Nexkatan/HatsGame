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

    public HexCell currentCell;
    public HexCell landCell;

    private Vector3 thisHatRot;
    private int thisHatRotInt;

    [SerializeField] HexCoordinates cellCo;

    public bool isValid = true;
    private bool SWValid = true;
    private bool WValid = true;
    private bool NWValid = true;
    private bool NEValid = true;
    private bool EValid = true;
    private bool SEValid = true;
    private bool longValid = true;
    private bool long1Valid = true;
    private bool long2Valid = true;

    private GameManager gameManager;

    private float checkTime;
    void Start()
    {
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        screenCamera = Camera.main;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        landCell = hexGrid.GetCell(transform.position);
        currentCell = landCell;
        checkTime = gameManager.difficulty;
        Debug.Log((1 / checkTime )/ 2);
        StartCoroutine(CheckValid(1 / checkTime));
    }

    void FixedUpdate()
    {
        if (isSelected)
        { 
            //MouseMove();
        }
    }

    private void Update()
    {
        HexMove();
        HexSpin();
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

    void HexMove()
    {
        thisHatRot = transform.eulerAngles;
        if (Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.W))
        {
            currentCell.hasHat = false;
            currentCell.hasReverseHat = false;
            landCell = currentCell.GetNeighbor(HexDirection.NW);

            if (!landCell.hasHat && !landCell.hasReverseHat)
            {
                IsPlacementValid();

                if (isValid)
                {
                    this.transform.position = landCell.transform.position;
                    currentCell = landCell;
                    if (this.CompareTag("Hat"))
                    {
                        landCell.hasHat = true;
                    }
                    else if (this.CompareTag("Reverse Hat"))
                    {
                        landCell.hasReverseHat = true;
                    }
                    landCell.hatRot = Mathf.Round(transform.eulerAngles.y);
                }
                else
                {
                    Debug.Log("Invalid");
                    if (CompareTag("Hat"))
                    {
                        currentCell.hasHat = true;
                    }
                    else if (CompareTag("Reverse Hat"))
                    {
                        currentCell.hasReverseHat = true;
                    }
                }
            }
            else if (landCell.hasHat || landCell.hasReverseHat)
            {
                Debug.Log("Invalid");
                if (CompareTag("Hat"))
                {
                    currentCell.hasHat = true;
                }
                else if (CompareTag("Reverse Hat"))
                {
                    currentCell.hasReverseHat = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.E))
        {


            currentCell.hasHat = false;
            currentCell.hasReverseHat = false;
            landCell = currentCell.GetNeighbor(HexDirection.NE);

            if (!landCell.hasHat && !landCell.hasReverseHat)
            {
                IsPlacementValid();

                if (isValid)
                {
                    this.transform.position = landCell.transform.position;
                    currentCell = landCell;
                    if (this.CompareTag("Hat"))
                    {
                        landCell.hasHat = true;
                    }
                    else if (this.CompareTag("Reverse Hat"))
                    {
                        landCell.hasReverseHat = true;
                    }
                    landCell.hatRot = Mathf.Round(transform.eulerAngles.y);
                }
                else
                {
                    Debug.Log("Invalid");
                    if (CompareTag("Hat"))
                    {
                        currentCell.hasHat = true;
                    }
                    else if (CompareTag("Reverse Hat"))
                    {
                        currentCell.hasReverseHat = true;
                    }
                }
            }
            else if (landCell.hasHat || landCell.hasReverseHat)
            {
                Debug.Log("Invalid");
                if (CompareTag("Hat"))
                {
                    currentCell.hasHat = true;
                }
                else if (CompareTag("Reverse Hat"))
                {
                    currentCell.hasReverseHat = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.D))
        {

            currentCell.hasHat = false;
            currentCell.hasReverseHat = false;
            landCell = currentCell.GetNeighbor(HexDirection.E);

            if (!landCell.hasHat && !landCell.hasReverseHat)
            {
                IsPlacementValid();

                if (isValid)
                {
                    this.transform.position = landCell.transform.position;
                    currentCell = landCell;
                    if (this.CompareTag("Hat"))
                    {
                        landCell.hasHat = true;
                    }
                    else if (this.CompareTag("Reverse Hat"))
                    {
                        landCell.hasReverseHat = true;
                    }
                    landCell.hatRot = Mathf.Round(transform.eulerAngles.y);
                }
                else
                {
                    Debug.Log("Invalid");
                    if (CompareTag("Hat"))
                    {
                        currentCell.hasHat = true;
                    }
                    else if (CompareTag("Reverse Hat"))
                    {
                        currentCell.hasReverseHat = true;
                    }
                }
            }
            else if (landCell.hasHat || landCell.hasReverseHat)
            {
                Debug.Log("Invalid");
                if (CompareTag("Hat"))
                {
                    currentCell.hasHat = true;
                }
                else if (CompareTag("Reverse Hat"))
                {
                    currentCell.hasReverseHat = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.X))
        {

            currentCell.hasHat = false;
            currentCell.hasReverseHat = false;
            landCell = currentCell.GetNeighbor(HexDirection.SE);

            if (!landCell.hasHat && !landCell.hasReverseHat)
            {
                IsPlacementValid();

                if (isValid)
                {
                    this.transform.position = landCell.transform.position;
                    currentCell = landCell;
                    if (this.CompareTag("Hat"))
                    {
                        landCell.hasHat = true;
                    }
                    else if (this.CompareTag("Reverse Hat"))
                    {
                        landCell.hasReverseHat = true;
                    }
                    landCell.hatRot = Mathf.Round(transform.eulerAngles.y);
                }
                else
                {
                    Debug.Log("Invalid");
                    if (CompareTag("Hat"))
                    {
                        currentCell.hasHat = true;
                    }
                    else if (CompareTag("Reverse Hat"))
                    {
                        currentCell.hasReverseHat = true;
                    }
                }
            }
            else if (landCell.hasHat || landCell.hasReverseHat)
            {
                Debug.Log("Invalid");
                if (CompareTag("Hat"))
                {
                    currentCell.hasHat = true;
                }
                else if (CompareTag("Reverse Hat"))
                {
                    currentCell.hasReverseHat = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Z))
        {
            currentCell.hasHat = false;
            currentCell.hasReverseHat = false;
            landCell = currentCell.GetNeighbor(HexDirection.SW);

            if (!landCell.hasHat && !landCell.hasReverseHat)
            {
                IsPlacementValid();

                if (isValid)
                {
                    this.transform.position = landCell.transform.position;
                    currentCell = landCell;
                    if (this.CompareTag("Hat"))
                    {
                        landCell.hasHat = true;
                    }
                    else if (this.CompareTag("Reverse Hat"))
                    {
                        landCell.hasReverseHat = true;
                    }
                    landCell.hatRot = Mathf.Round(transform.eulerAngles.y);
                }
                else
                {
                    Debug.Log("Invalid");
                    if (CompareTag("Hat"))
                    {
                        currentCell.hasHat = true;
                    }
                    else if (CompareTag("Reverse Hat"))
                    {
                        currentCell.hasReverseHat = true;
                    }
                }
            }
            else if (landCell.hasHat || landCell.hasReverseHat)
            {
                Debug.Log("Invalid");
                if (CompareTag("Hat"))
                {
                    currentCell.hasHat = true;
                }
                else if (CompareTag("Reverse Hat"))
                {
                    currentCell.hasReverseHat = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.A))
        {
            currentCell.hasHat = false;
            currentCell.hasReverseHat = false;
            landCell = currentCell.GetNeighbor(HexDirection.W);

            if (!landCell.hasHat && !landCell.hasReverseHat)
            {
                IsPlacementValid();

                if (isValid)
                {
                    this.transform.position = landCell.transform.position;
                    currentCell = landCell;
                    if (this.CompareTag("Hat"))
                    {
                        landCell.hasHat = true;
                    }
                    else if (this.CompareTag("Reverse Hat"))
                    {
                        landCell.hasReverseHat = true;
                    }
                    landCell.hatRot = Mathf.Round(transform.eulerAngles.y);
                }
                else
                {
                    Debug.Log("Invalid");
                    if (CompareTag("Hat"))
                    {
                        currentCell.hasHat = true;
                    }
                    else if (CompareTag("Reverse Hat"))
                    {
                        currentCell.hasReverseHat = true;
                    }
                }
            }
            else if (landCell.hasHat || landCell.hasReverseHat)
            {
                Debug.Log("Invalid");
                if (CompareTag("Hat"))
                {
                    currentCell.hasHat = true;
                }
                else if (CompareTag("Reverse Hat"))
                {
                    currentCell.hasReverseHat = true;
                }
            }
        }
    }

    void HexSpin()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.J)) 
        {
            Vector3 m_EulerAngleVelocity = new Vector3(0, 60, 0);
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity);
            thisHatRot += m_EulerAngleVelocity;
            Debug.Log(thisHatRot);

            IsPlacementValid();

            if (isValid)
            {
                this.transform.rotation *= deltaRotation;
            }
            else
            {
                thisHatRot -= m_EulerAngleVelocity;
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
        landCell = hexGrid.GetCell(transform.position);
        currentCell = landCell;
        if (!landCell.hasHat && !landCell.hasReverseHat)
        {
            IsPlacementValid();

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
                if (landCell.hatRot == 360)
                {
                    landCell.hatRotInt = 0;
                }
                else
                {
                    landCell.hatRotInt = Mathf.RoundToInt(landCell.hatRot / 60);
                }
                
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

    public void IsPlacementValid()
    {
        HexCell neighborNE = landCell.GetNeighbor(HexDirection.NE);
        HexCell neighborE = landCell.GetNeighbor(HexDirection.E);
        HexCell neighborSE = landCell.GetNeighbor(HexDirection.SE);
        HexCell neighborSW = landCell.GetNeighbor(HexDirection.SW);
        HexCell neighborW = landCell.GetNeighbor(HexDirection.W);
        HexCell neighborNW = landCell.GetNeighbor(HexDirection.NW);

        HexCell neighborLong;
        HexCell neighborLongReverse1;
        HexCell neighborLongReverse2;

        


        if (Mathf.Round(thisHatRot.y) == 0 || Mathf.Round(thisHatRot.y) == 360)
        {
            if (this.CompareTag("Hat"))
            {
                if (neighborSW.hasHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 0 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240 || neighborSW.hatRot == 300)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
                if (neighborW.hasHat)
                {
                    Debug.Log("W Hat");
                    if (neighborW.hatRot == 300)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
                if (neighborNW.hasHat)
                {
                    Debug.Log("NW hat");
                    if (neighborNW.hatRot == 60)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
                if (neighborNE.hasHat)
                {
                    Debug.Log("NE Hat");
                    if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 120 || neighborNE.hatRot == 180)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
                if (neighborE.hasHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 180 || neighborE.hatRot == 240 || neighborE.hatRot == 300)
                    {
                        Debug.Log("Accept");
                        EValid = true;
                    }
                    else
                    {
                        Debug.Log("else");
                        EValid = false;
                    }
                }
                if (neighborSE.hasHat)
                {
                    Debug.Log("SE Hit");
                    if (neighborSE.hatRot == 60 || neighborSE.hatRot == 120 || neighborSE.hatRot == 180 || neighborSE.hatRot == 240 || neighborSE.hatRot == 300)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

                if (neighborSW.hasReverseHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 0 || neighborSW.hatRot == 60 || neighborSW.hatRot == 120 || neighborSW.hatRot == 180)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
                if (neighborW.hasReverseHat)
                {
                    Debug.Log("W Hat");
                    if (neighborW.hatRot == 240)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
                if (neighborNW.hasReverseHat)
                {
                    Debug.Log("NW hat");
                    if (neighborNW.hatRot == 0)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
                if (neighborNE.hasReverseHat)
                {
                    Debug.Log("NE Hat");
                    if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 240 || neighborNE.hatRot == 300)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
                if (neighborE.hasReverseHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 0 || neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 300)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
                if (neighborSE.hasReverseHat)
                {
                    if (neighborSE.hatRot == 0 || neighborSE.hatRot == 60 || neighborSE.hatRot == 120 || neighborSE.hatRot == 180 || neighborSE.hatRot == 240)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

                neighborLong = neighborNW.GetNeighbor(HexDirection.W);
                if (neighborLong.hasHat)
                {
                    if (neighborLong.hatRot == 180)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                else if (neighborLong.hasReverseHat)
                {
                    if (neighborLong.hatRot == 0 || neighborLong.hatRot == 60)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                neighborLongReverse1 = neighborNW.GetNeighbor(HexDirection.NW);
                if (neighborLongReverse1.hasReverseHat)
                {
                    if (neighborLongReverse1.hatRot == 120)
                    {
                        long1Valid = false;
                    }
                    else
                    {
                        long1Valid = true;
                    }
                }
                neighborLongReverse2 = neighborSW.GetNeighbor(HexDirection.W);
                if (neighborLongReverse2.hasReverseHat)
                {
                    if (neighborLongReverse2.hatRot == 0)
                    {
                        long2Valid = false;
                    }
                    else
                    {
                        long2Valid = true;
                    }
                }



            }
            else if (this.CompareTag("Reverse Hat"))
            {


                if (neighborSW.hasHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 0 || neighborSW.hatRot == 120 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240 || neighborSW.hatRot == 300)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
                if (neighborW.hasHat)
                {
                    Debug.Log("W Hat");
                    if (neighborW.hatRot == 0 || neighborW.hatRot == 60 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
                if (neighborNW.hasHat)
                {
                    Debug.Log("NW hat");
                    if (neighborNW.hatRot == 0 || neighborNW.hatRot == 60 || neighborNW.hatRot == 120 || neighborNW.hatRot == 300)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
                if (neighborNE.hasHat)
                {
                    Debug.Log("NE Hat");
                    if (neighborNE.hatRot == 0)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
                if (neighborE.hasHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 120)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
                if (neighborSE.hasHat)
                {
                    Debug.Log("SE Hit");
                    if (neighborSE.hatRot == 0 || neighborSE.hatRot == 180 || neighborSE.hatRot == 240 || neighborSE.hatRot == 300)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

                if (neighborSW.hasReverseHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 60 || neighborSW.hatRot == 120 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240 || neighborSW.hatRot == 300)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
                if (neighborW.hasReverseHat)
                {
                    Debug.Log("W Hat");
                    if (neighborW.hatRot == 60 || neighborW.hatRot == 120 || neighborW.hatRot == 180 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
                if (neighborNW.hasReverseHat)
                {
                    Debug.Log("NW hat");
                    if (neighborNW.hatRot == 0 || neighborNW.hatRot == 180 || neighborNW.hatRot == 240 || neighborNW.hatRot == 300)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
                if (neighborNE.hasReverseHat)
                {
                    Debug.Log("NE Hat");
                    if (neighborNE.hatRot == 300)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
                if (neighborE.hasReverseHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 60)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
                if (neighborSE.hasReverseHat)
                {
                    if (neighborSE.hatRot == 0 || neighborSE.hatRot == 60 || neighborSE.hatRot == 120 || neighborSE.hatRot == 180)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

                neighborLong = neighborNE.GetNeighbor(HexDirection.E);
                if (neighborLong.hasReverseHat)
                {
                    Debug.Log("NE_E Reverse Hat");
                    if (neighborLong.hatRot == 180)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                else if (neighborLong.hasHat)
                {
                    if (neighborLong.hatRot == 0 || neighborLong.hatRot == 300)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                neighborLongReverse1 = neighborSE.GetNeighbor(HexDirection.E);
                if (neighborLongReverse1.hasHat)
                {
                    if (neighborLongReverse1.hatRot == 0)
                    {
                        long1Valid = false;
                    }
                    else
                    {
                        long1Valid = true;
                    }
                }
                neighborLongReverse2 = neighborNE.GetNeighbor(HexDirection.NE);
                if (neighborLongReverse2.hasHat)
                {
                    if (neighborLongReverse2.hatRot == 240)
                    {
                        long2Valid = false;
                    }
                    else
                    {
                        long2Valid = true;
                    }
                }
            }
            
        }

        else if (Mathf.Round(thisHatRot.y) == 60)
        {
            if (this.CompareTag("Hat"))
            {
            if (neighborSW.hasHat)
                {
                    Debug.Log("SW Hit");
                    if (neighborSW.hatRot == 0 || neighborSW.hatRot == 120 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240 || neighborSW.hatRot == 300)
                {
                    SWValid = true;
                }
                else
                {
                    SWValid = false;
                }
            }
            if (neighborW.hasHat)
            {
                    Debug.Log("W Hit");
                    if (neighborW.hatRot == 0 || neighborW.hatRot == 60 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                {
                    WValid = true;
                }
                else
                {
                    WValid = false;
                }
            }
            if (neighborNW.hasHat)
            {
                Debug.Log("NW Hat");
                if (neighborNW.hatRot == 0)
                {
                    NWValid = true;
                }
                else
                {
                    NWValid = false;
                }
            }
            if (neighborNE.hasHat)
            {
                Debug.Log("NE hat");
                if (neighborNE.hatRot == 120)
                {
                    NEValid = true;
                }
                else
                {
                    NEValid = false;
                }
            }
            if (neighborE.hasHat)
            {
                Debug.Log("E Hat");
                if (neighborE.hatRot == 60 || neighborE.hatRot == 180 || neighborE.hatRot == 240)
                {
                    EValid = true;
                }
                else
                {
                    EValid = false;
                }
            }
            if (neighborSE.hasHat)
            {
                Debug.Log("SE Hat");
                if (neighborSE.hatRot == 0 || neighborSE.hatRot == 120 || neighborSE.hatRot == 180 || neighborSE.hatRot == 240 || neighborSE.hatRot == 300)
                {
                    SEValid = true;
                }
                else
                {
                    SEValid = false;
                }
            }

            if (neighborSW.hasReverseHat)
            {
                Debug.Log("SW Hat");
                if (neighborSW.hatRot == 60 || neighborSW.hatRot == 120 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240 || neighborSW.hatRot == 300)
                {
                    SWValid = true;
                }
                else
                {
                    SWValid = false;
                }
            }
            if (neighborW.hasReverseHat)
            {
                Debug.Log("W Hat");
                if (neighborW.hatRot == 60 || neighborW.hatRot == 120 || neighborW.hatRot == 180 || neighborW.hatRot == 240)
                {
                    WValid = true;
                }
                else
                {
                    WValid = false;
                }
            }
            if (neighborNW.hasReverseHat)
            {
                Debug.Log("NW hat");
                if (neighborNW.hatRot == 300)
                {
                    NWValid = true;
                }
                else
                {
                    NWValid = false;
                }
            }
            if (neighborNE.hasReverseHat)
            {
                Debug.Log("NE Hat");
                if (neighborNE.hatRot == 60)
                {
                    NEValid = true;
                }
                else
                {
                    NEValid = false;
                }
            }
            if (neighborE.hasReverseHat)
            {
                Debug.Log("E Hat");
                if (neighborE.hatRot == 0 || neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 300)
                {
                    EValid = true;
                }
                else
                {
                    EValid = false;
                }
            }
            if (neighborSE.hasReverseHat)
            {
                Debug.Log("SE hat");
                if (neighborSE.hatRot == 0 || neighborSE.hatRot == 60 || neighborSE.hatRot == 120 || neighborSE.hatRot == 180)
                {
                    SEValid = true;
                }
                else
                {
                    SEValid = false;
                }
            }

                neighborLong = neighborNE.GetNeighbor(HexDirection.NW);
                if (neighborLong.hasHat)
                {
                    if (neighborLong.hatRot == 240)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                else if (neighborLong.hasReverseHat)
                {
                    if (neighborLong.hatRot == 60 || neighborLong.hatRot == 120)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                neighborLongReverse1 = neighborNE.GetNeighbor(HexDirection.NE);
                if (neighborLongReverse1.hasReverseHat)
                {
                    if (neighborLongReverse1.hatRot == 180)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                neighborLongReverse2 = neighborNW.GetNeighbor(HexDirection.W);
                if (neighborLongReverse2.hasReverseHat)
                {
                    if (neighborLongReverse2.hatRot == 60)
                    {
                        long2Valid = false;
                    }
                    else
                    {
                        long2Valid = true;
                    }
                }

            }
            else if (this.CompareTag("Reverse Hat"))
            {
                if (neighborSW.hasHat)
                {
                    Debug.Log("SW Hit");
                    if (neighborSW.hatRot == 0 || neighborSW.hatRot == 60 || neighborSW.hatRot == 240 || neighborSW.hatRot == 300)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
                if (neighborW.hasHat)
                {
                    Debug.Log("W Hit");
                    if (neighborW.hatRot == 0 || neighborW.hatRot == 60 || neighborW.hatRot == 180 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
                if (neighborNW.hasHat)
                {
                    Debug.Log("NW Hat");
                    if (neighborNW.hatRot == 0 || neighborNW.hatRot == 60 || neighborNW.hatRot == 120 || neighborNW.hatRot == 300)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
                if (neighborNE.hasHat)
                {
                    Debug.Log("NE hat");
                    if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 120 || neighborNE.hatRot == 180)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
                if (neighborE.hasHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 60)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
                if (neighborSE.hasHat)
                {
                    Debug.Log("SE Hat");
                    if (neighborSE.hatRot == 120 || neighborSE.hatRot == 180 || neighborSE.hatRot == 240 || neighborSE.hatRot == 300)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

                if (neighborSW.hasReverseHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 60 || neighborSW.hatRot == 120 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
                if (neighborW.hasReverseHat)
                {
                    Debug.Log("W Hat");
                    if (neighborW.hatRot == 0 || neighborW.hatRot == 120 || neighborW.hatRot == 180 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
                if (neighborNW.hasReverseHat)
                {
                    Debug.Log("NW hat");
                    if (neighborNW.hatRot == 0 || neighborNW.hatRot == 120 || neighborNW.hatRot == 180 || neighborNW.hatRot == 240 || neighborNW.hatRot == 300)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
                if (neighborNE.hasReverseHat)
                {
                    Debug.Log("NE Hat");
                    if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 240 || neighborNE.hatRot == 300)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
                if (neighborE.hasReverseHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 0)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
                if (neighborSE.hasReverseHat)
                {
                    Debug.Log("SE hat");
                    if (neighborSE.hatRot == 120)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }


                neighborLong = neighborE.GetNeighbor(HexDirection.SE);
                if (neighborLong.hasReverseHat)
                {
                    if (neighborLong.hatRot == 240)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                else if (neighborLong.hasHat)
                {
                    if (neighborLong.hatRot == 0 || neighborLong.hatRot == 60)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                neighborLongReverse1 = neighborSE.GetNeighbor(HexDirection.SW);
                if (neighborLongReverse1.hasHat)
                {
                    if (neighborLongReverse1.hatRot == 60)
                    {
                        long1Valid = false;
                    }
                    else
                    {
                        long1Valid = true;
                    }
                }
                neighborLongReverse2 = neighborNE.GetNeighbor(HexDirection.NE);
                if (neighborLongReverse2.hasHat)
                {
                    if (neighborLongReverse2.hatRot == 240)
                    {
                        long2Valid = false;
                    }
                    else
                    {
                        long2Valid = true;
                    }
                }
            }

            
        }

        else if (Mathf.Round(thisHatRot.y) == 120)
        {
            if(this.CompareTag("Hat"))
            {
            if (neighborSW.hasHat)
            {
                Debug.Log("SW Hat");
                if (neighborSW.hatRot == 0 || neighborSW.hatRot == 60 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240 || neighborSW.hatRot == 300)
                {
                    SWValid = true;
                }
                else
                {
                    SWValid = false;
                }
            }
            if (neighborW.hasHat)
            {
                    Debug.Log("W Hit");
                    if (neighborW.hatRot == 0 || neighborW.hatRot == 60 || neighborW.hatRot == 180 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                {
                    WValid = true;
                }
                else
                {
                    WValid = false;
                }
            }
            if (neighborNW.hasHat)
            {
                    Debug.Log("NW Hit");
                    if (neighborNW.hatRot == 0 || neighborNW.hatRot == 60 || neighborNW.hatRot == 120 || neighborNW.hatRot == 300)
                {
                    NWValid = true;
                }
                else
                {
                    NWValid = false;
                }
            }
            if (neighborNE.hasHat)
            {
                Debug.Log("NE Hat");
                if (neighborNE.hatRot == 60)
                {
                    NEValid = true;
                }
                else
                {
                    NEValid = false;
                }
            }
            if (neighborE.hasHat)
            {
                Debug.Log("E hat");
                if (neighborE.hatRot == 180)
                {
                    EValid = true;
                }
                else
                {
                    EValid = false;
                }
            }
            if (neighborSE.hasHat)
            {
                Debug.Log("SE Hat");
                if (neighborSE.hatRot == 120 || neighborSE.hatRot == 180 || neighborSE.hatRot == 240 || neighborSE.hatRot == 300)
                {
                    SEValid = true;
                }
                else
                {
                    SEValid = false;
                }
            }

            if (neighborSW.hasReverseHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 60 || neighborSW.hatRot == 120 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
            if (neighborW.hasReverseHat)
                {
                    Debug.Log("W Hat");
                    if (neighborW.hatRot == 0 || neighborW.hatRot == 120 || neighborW.hatRot == 180 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
            if (neighborNW.hasReverseHat)
                {
                    Debug.Log("NW hat");
                    if (neighborNW.hatRot == 120 || neighborNW.hatRot == 180 || neighborNW.hatRot == 240 || neighborNW.hatRot == 300)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
            if (neighborNE.hasReverseHat)
                {
                    Debug.Log("NE Hat");
                    if (neighborNE.hatRot == 0)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
            if (neighborE.hasReverseHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 120)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
            if (neighborSE.hasReverseHat)
                {
                    if (neighborSE.hatRot == 0 || neighborSE.hatRot == 60 || neighborSE.hatRot == 120 || neighborSE.hatRot == 180)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

                neighborLong = neighborNE.GetNeighbor(HexDirection.E);
                if (neighborLong.hasHat)
                {
                    if (neighborLong.hatRot == 300)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                else if (neighborLong.hasReverseHat)
                {
                    if (neighborLong.hatRot == 120 || neighborLong.hatRot == 180)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                neighborLongReverse1 = neighborNW.GetNeighbor(HexDirection.NE);
                if (neighborLongReverse1.hasReverseHat)
                {
                    if ( neighborLongReverse1.hatRot == 120)
                    {
                        long1Valid = false;
                    }
                    else
                    {
                        long1Valid = true;
                    }
                }
                neighborLongReverse2 = neighborE.GetNeighbor(HexDirection.E);
                if (neighborLongReverse2.hasReverseHat)
                {
                    if (neighborLongReverse2.hatRot == 240)
                    {
                        long2Valid = false;
                    }
                    else
                    {
                        long2Valid = true;
                    }
                }


            }
            else if (this.CompareTag("Reverse Hat"))
            {
                if (neighborSW.hasHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 240)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
                if (neighborW.hasHat)
                {
                    Debug.Log("W Hit");
                    if (neighborW.hatRot == 0 || neighborW.hatRot == 60 || neighborW.hatRot == 120 || neighborW.hatRot == 300)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
                if (neighborNW.hasHat)
                {
                    Debug.Log("NW Hit");
                    if (neighborNW.hatRot == 0 || neighborNW.hatRot == 60 || neighborNW.hatRot == 120 || neighborNW.hatRot == 240 || neighborNW.hatRot == 300)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
                if (neighborNE.hasHat)
                {
                    Debug.Log("NE Hat");
                    if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 120 || neighborNE.hatRot == 180)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
                if (neighborE.hasHat)
                {
                    Debug.Log("E hat");
                    if (neighborE.hatRot == 60 ||neighborE.hatRot == 120 || neighborE.hatRot == 180 || neighborE.hatRot == 240)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
                if (neighborSE.hasHat)
                {
                    Debug.Log("SE Hat");
                    if (neighborSE.hatRot == 120)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

                if (neighborSW.hasReverseHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 180)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
                if (neighborW.hasReverseHat)
                {
                    Debug.Log("W Hat");
                    if (neighborW.hatRot == 0 || neighborW.hatRot == 120 || neighborW.hatRot == 180 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
                if (neighborNW.hasReverseHat)
                {
                    Debug.Log("NW hat");
                    if (neighborNW.hatRot == 0 || neighborNW.hatRot == 60 || neighborNW.hatRot == 180 || neighborNW.hatRot == 240 || neighborNW.hatRot == 300)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
                if (neighborNE.hasReverseHat)
                {
                    Debug.Log("NE Hat");
                    if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 180 || neighborNE.hatRot == 240 || neighborNE.hatRot == 300)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
                if (neighborE.hasReverseHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 0 || neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 300)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
                if (neighborSE.hasReverseHat)
                {
                    if (neighborSE.hatRot == 60)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

                neighborLong = neighborSE.GetNeighbor(HexDirection.SW);
                if (neighborLong.hasReverseHat)
                {
                    if (neighborLong.hatRot == 300)
                    {
                        long1Valid = false;
                    }
                    else
                    {
                        long1Valid = true;
                    }
                }
                else if (neighborLong.hasHat)
                {
                    if (neighborLong.hatRot == 60 || neighborLong.hatRot == 120)
                    {
                        long1Valid = false;
                    }
                    else
                    {
                        long1Valid = true;
                    }
                }
                neighborLongReverse1 = neighborSW.GetNeighbor(HexDirection.W);
                if (neighborLongReverse1.hasHat)
                {
                    if (neighborLongReverse1.hatRot == 120)
                    {
                        long1Valid = false;
                    }
                    else
                    {
                        long1Valid = true;
                    }
                }
                neighborLongReverse2 = neighborSE.GetNeighbor(HexDirection.SE);
                if (neighborLongReverse2.hasHat)
                {
                    if (neighborLongReverse2.hatRot == 0)
                    {
                        long1Valid = false;
                    }
                    else
                    {
                        long1Valid = true;
                    }
                }
            }
            
        }

        else if (Mathf.Round(thisHatRot.y) == 180)
        {
            if (this.CompareTag("Hat"))
            {
            if (neighborSW.hasHat)
            {
                Debug.Log("SW Hat");
                if (neighborSW.hatRot == 0 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240 || neighborSW.hatRot == 300)
                {
                    SWValid = true;
                }
                else
                {
                    SWValid = false;
                }
            }
            if (neighborW.hasHat)
            {
                Debug.Log("W Hat");
                if (neighborW.hatRot == 0 || neighborW.hatRot == 60 || neighborW.hatRot == 120 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                {
                    WValid = true;
                }
                else
                {
                    WValid = false;
                }
            }
            if (neighborNW.hasHat)
            {
                    Debug.Log("NW Hit");
                    if (neighborNW.hatRot == 0 || neighborNW.hatRot == 60 || neighborNW.hatRot == 120 || neighborNW.hatRot == 240 || neighborNW.hatRot == 300)
                {
                    NWValid = true;
                }
                else
                {
                    NWValid = false;
                }
            }
            if (neighborNE.hasHat)
            {
                    Debug.Log("NE Hit");
                    if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 120 || neighborNE.hatRot == 180)
                {
                    NEValid = true;
                }
                else
                {
                    NEValid = false;
                }
            }
            if (neighborE.hasHat)
            {
                Debug.Log("E Hat");
                if (neighborE.hatRot == 120)
                {
                    EValid = true;
                }
                else
                {
                    EValid = false;
                }
            }
            if (neighborSE.hasHat)
            {
                Debug.Log("SE hat");
                if (neighborSE.hatRot == 240)
                {
                    SEValid = true;
                }
                else
                {
                    SEValid = false;
                }
            }

            if (neighborSW.hasReverseHat)
            {
                Debug.Log("SW Hat");
                if (neighborSW.hatRot == 60 || neighborSW.hatRot == 120 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240)
                {
                    SWValid = true;
                }
                else
                {
                    SWValid = false;
                }
            }
            if (neighborW.hasReverseHat)
            {
                Debug.Log("W Hat");
                if (neighborW.hatRot == 120 || neighborW.hatRot == 180 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                {
                    WValid = true;
                }
                else
                {
                    WValid = false;
                }
            }
            if (neighborNW.hasReverseHat)
            {
                Debug.Log("NW hat");
                if (neighborNW.hatRot == 0 || neighborNW.hatRot == 60 || neighborNW.hatRot == 180 || neighborNW.hatRot == 240 || neighborNW.hatRot == 300)
                {
                    NWValid = true;
                }
                else
                {
                    NWValid = false;
                }
            }
            if (neighborNE.hasReverseHat)
            {
                Debug.Log("NE Hat");
                if (neighborNE.hatRot == 0 || neighborNE.hatRot == 240 || neighborNE.hatRot == 180 || neighborNE.hatRot == 300)
                {
                    NEValid = true;
                }
                else
                {
                    NEValid = false;
                }
            }
            if (neighborE.hasReverseHat)
            {
                Debug.Log("E Hat");
                if (neighborE.hatRot == 60)
                {
                    EValid = true;
                }
                else
                {
                    EValid = false;
                }
            }
            if (neighborSE.hasReverseHat)
            {
                Debug.Log("SE Hat");
                if (neighborSE.hatRot == 180)
                {
                    SEValid = true;
                }
                else
                {
                    SEValid = false;
                }
            }

                neighborLong = neighborSE.GetNeighbor(HexDirection.E);
                if (neighborLong.hasHat)
                {
                    if (neighborLong.hatRot == 0)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                else if (neighborLong.hasReverseHat)
                {
                    if (neighborLong.hatRot == 180 || neighborLong.hatRot == 240)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                neighborLongReverse1 = neighborNE.GetNeighbor(HexDirection.E);
                if (neighborLongReverse1.hasReverseHat)
                {
                    if (neighborLongReverse1.hatRot == 180)
                    {
                        long1Valid = false;
                    }
                    else
                    {
                        long1Valid = true;
                    }
                }
                neighborLongReverse2 = neighborSE.GetNeighbor(HexDirection.SE);
                if (neighborLongReverse2.hasReverseHat)
                {
                    if (neighborLongReverse2.hatRot == 300)
                    {
                        long2Valid = false;
                    }
                    else
                    {
                        long2Valid = true;
                    }
                }

            }
            else if (this.CompareTag("Reverse Hat"))
            {
                if (neighborSW.hasHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 180)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
                if (neighborW.hasHat)
                {
                    Debug.Log("W Hat");
                    if (neighborW.hatRot == 300)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
                if (neighborNW.hasHat)
                {
                    Debug.Log("NW Hit");
                    if (neighborNW.hatRot == 0 || neighborNW.hatRot == 60 || neighborNW.hatRot == 120 || neighborNW.hatRot == 180)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
                if (neighborNE.hasHat)
                {
                    Debug.Log("NE Hit");
                    if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 120 || neighborNE.hatRot == 180 || neighborNE.hatRot == 300)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
                if (neighborE.hasHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 180 || neighborE.hatRot == 240)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
                if (neighborSE.hasHat)
                {
                    Debug.Log("SE hat");
                    if (neighborSE.hatRot == 120 || neighborSE.hatRot == 180 || neighborSE.hatRot == 240 || neighborSE.hatRot == 300)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

                if (neighborSW.hasReverseHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 120)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
                if (neighborW.hasReverseHat)
                {
                    Debug.Log("W Hat");
                    if (neighborW.hatRot == 240)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
                if (neighborNW.hasReverseHat)
                {
                    Debug.Log("NW hat");
                    if (neighborNW.hatRot == 0 || neighborNW.hatRot == 180 || neighborNW.hatRot == 240 || neighborNW.hatRot == 300)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
                if (neighborNE.hasReverseHat)
                {
                    Debug.Log("NE Hat");
                    if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 120 || neighborNE.hatRot == 240 || neighborNE.hatRot == 300)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
                if (neighborE.hasReverseHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 0 || neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 240 || neighborE.hatRot == 300)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
                if (neighborSE.hasReverseHat)
                {
                    Debug.Log("SE Hat");
                    if (neighborSE.hatRot == 0 || neighborSE.hatRot == 60 || neighborSE.hatRot == 120 || neighborSE.hatRot == 180)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

                neighborLong = neighborSW.GetNeighbor(HexDirection.W);
                if (neighborLong.hasReverseHat)
                {
                    if (neighborLong.hatRot == 0)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                else if (neighborLong.hasHat)
                {
                    if (neighborLong.hatRot == 120 || neighborLong.hatRot == 180)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                neighborLongReverse1 = neighborSW.GetNeighbor(HexDirection.SW);
                if (neighborLongReverse1.hasHat)
                {
                    if (neighborLongReverse1.hatRot == 60) 
                    { 
                        longValid = false; 
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                neighborLongReverse2 = neighborNW.GetNeighbor(HexDirection.W);
                if (neighborLongReverse2.hasHat)
                {
                    if (neighborLongReverse2.hatRot == 180)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
            }
            
            
        }

        else if (Mathf.Round(thisHatRot.y) == 240)
        {
            if (this.CompareTag("Hat"))
            {
            if (neighborSW.hasHat)
            {
                Debug.Log("SW hat");
                if (neighborSW.hatRot == 300)
                {
                    SWValid = true;
                }
                else
                {
                    SWValid = false;
                }
            }
            if (neighborW.hasHat)
            {
                Debug.Log("W Hat");
                if (neighborW.hatRot == 0 || neighborW.hatRot == 60 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                {
                    WValid = true;
                }
                else
                {
                    WValid = false;
                }
            }
            if (neighborNW.hasHat)
            {
                Debug.Log("NW Hat");
                if (neighborNW.hatRot == 0 || neighborNW.hatRot == 60 || neighborNW.hatRot == 120 || neighborNW.hatRot == 180 || neighborNW.hatRot == 300)
                {
                    NWValid = true;
                }
                else
                {
                    NWValid = false;
                }
            }
            if (neighborNE.hasHat)
            {
                    Debug.Log("NE Hit");
                    if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 120 || neighborNE.hatRot == 180 || neighborNE.hatRot == 300)
                {
                    NEValid = true;
                }
                else
                {
                    NEValid = false;
                }
            }
            if (neighborE.hasHat)
                {
                    Debug.Log("E Hit");
                    if (neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 180 || neighborE.hatRot == 240)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
            if (neighborSE.hasHat)
                {
                    Debug.Log("SE Hat");
                    if (neighborSE.hatRot == 180)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

            if (neighborSW.hasReverseHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 240)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
            if (neighborW.hasReverseHat)
                {
                    Debug.Log("W Hat");
                    if (neighborW.hatRot == 120 || neighborW.hatRot == 180 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
            if (neighborNW.hasReverseHat)
                {
                    Debug.Log("NW hat");
                    if (neighborNW.hatRot == 0 || neighborNW.hatRot == 120 || neighborNW.hatRot == 180 || neighborNW.hatRot == 240 || neighborNW.hatRot == 300)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
            if (neighborNE.hasReverseHat)
                {
                    Debug.Log("NE Hat");
                    if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 120 || neighborNE.hatRot == 240 || neighborNE.hatRot == 300)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
            if (neighborE.hasReverseHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 0 || neighborE.hatRot == 60 || neighborE.hatRot == 240 || neighborE.hatRot == 300)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
            if (neighborSE.hasReverseHat)
                {
                    if (neighborSE.hatRot == 60 || neighborSE.hatRot == 120)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

                neighborLong = neighborSW.GetNeighbor(HexDirection.SE);
                if (neighborLong.hasHat)
                {
                    if (neighborLong.hatRot == 60)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                else if (neighborLong.hasReverseHat)
                {
                    if (neighborLong.hatRot == 240 || neighborLong.hatRot == 300)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                neighborLongReverse1 = neighborE.GetNeighbor(HexDirection.SE);
                if (neighborLongReverse1.hasReverseHat)
                {
                    if (neighborLongReverse1.hatRot == 240)
                    {
                        long1Valid = false;
                    }
                    else
                    {
                        long1Valid = true;
                    }
                }
                neighborLongReverse2 = neighborSW.GetNeighbor(HexDirection.SW);
                if (neighborLongReverse2.hasReverseHat)
                {
                    if (neighborLongReverse2.hatRot == 0)
                    {
                        long2Valid = false;
                    }
                    else
                    {
                        long2Valid = true;
                    }
                }
            }
            else if (this.CompareTag("Reverse Hat"))
            {
                if (neighborSW.hasHat)
                {
                    Debug.Log("SW Hit");
                    if (neighborSW.hatRot == 0 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240 || neighborSW.hatRot == 300)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
                if (neighborW.hasHat)
                {
                    Debug.Log("W Hit");
                    if (neighborW.hatRot == 240)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
                if (neighborNW.hasHat)
                {
                    Debug.Log("NW Hat");
                    if (neighborNW.hatRot == 0)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
                if (neighborNE.hasHat)
                {
                    Debug.Log("NE hat");
                    if (neighborNE.hatRot == 60 || neighborNE.hatRot == 120 || neighborNE.hatRot == 180 || neighborNE.hatRot == 240)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
                if (neighborE.hasHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 0 || neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 180 || neighborE.hatRot == 240)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
                if (neighborSE.hasHat)
                {
                    Debug.Log("SE Hat");
                    if (neighborSE.hatRot == 120 || neighborSE.hatRot == 180 || neighborSE.hatRot == 240 || neighborSE.hatRot == 300)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

                if (neighborSW.hasReverseHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 60 || neighborSW.hatRot == 120 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
                if (neighborW.hasReverseHat)
                {
                    Debug.Log("W Hat");
                    if (neighborW.hatRot == 180)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
                if (neighborNW.hasReverseHat)
                {
                    Debug.Log("NW hat");
                    if (neighborNW.hatRot == 300)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
                if (neighborNE.hasReverseHat)
                {
                    Debug.Log("NE Hat");
                    if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 240 || neighborNE.hatRot == 300)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
                if (neighborE.hasReverseHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 0 || neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 180 || neighborE.hatRot == 300)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
                if (neighborSE.hasReverseHat)
                {
                    Debug.Log("SE hat");
                    if (neighborSE.hatRot == 0 || neighborSE.hatRot == 60 || neighborSE.hatRot == 120 || neighborSE.hatRot == 180 || neighborSE.hatRot == 300)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

                neighborLong = neighborNW.GetNeighbor(HexDirection.W);
                if (neighborLong.hasReverseHat)
                {
                    if (neighborLong.hatRot == 60)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                else if (neighborLong.hasHat)
                {
                    if (neighborLong.hatRot == 180 || neighborLong.hatRot == 240)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                neighborLongReverse1 = neighborNW.GetNeighbor(HexDirection.NE);
                if (neighborLongReverse1.hasHat) 
                {
                    if (neighborLongReverse1.hatRot == 240)
                    {
                        long1Valid = false;
                    }
                    else
                    {
                        long1Valid = true;
                    }
                }
                neighborLongReverse2 = neighborW.GetNeighbor(HexDirection.W);
                if (neighborLongReverse2.hasHat)
                {
                    if (neighborLongReverse2.hatRot == 120)
                    {
                        long2Valid = false;
                    }
                    else
                    {
                        long2Valid = true;
                    }
                }

            }

            
        }

        else if (Mathf.Round(thisHatRot.y) == 300)
        {
            if (this.CompareTag("Hat"))
            {
                
            if (neighborSW.hasHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 240)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
            if (neighborW.hasHat)
                {
                    Debug.Log("W hat");
                    if (neighborW.hatRot == 0)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
            if (neighborNW.hasHat)
                {
                    Debug.Log("NW Hat");
                    if (neighborNW.hatRot == 0 || neighborNW.hatRot == 60 || neighborNW.hatRot == 120 || neighborNW.hatRot == 300)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
            if (neighborNE.hasHat)
                {
                    Debug.Log("NE Hat");
                    if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 120 || neighborNE.hatRot == 180 || neighborNE.hatRot == 240)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
            if (neighborE.hasHat)
                {
                    Debug.Log("E Hit");
                    if (neighborE.hatRot == 0 || neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 180 || neighborE.hatRot == 240)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
            if(neighborSE.hasHat)
                {
                    Debug.Log("SE Hit");
                    if (neighborSE.hatRot == 120 || neighborSE.hatRot == 180 || neighborSE.hatRot == 240 || neighborSE.hatRot == 300)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

            if (neighborSW.hasReverseHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 180)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
            if (neighborW.hasReverseHat)
                {
                    Debug.Log("W Hat");
                    if (neighborW.hatRot == 300)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
            if (neighborNW.hasReverseHat)
                {
                    Debug.Log("NW hat");
                    if (neighborNW.hatRot == 0 || neighborNW.hatRot == 180 || neighborNW.hatRot == 240 || neighborNW.hatRot == 300)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
            if (neighborNE.hasReverseHat)
                {
                    Debug.Log("NE Hat");
                    if (neighborNE.hatRot == 0 || neighborNE.hatRot == 60 || neighborNE.hatRot == 240 || neighborNE.hatRot == 300)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
            if (neighborE.hasReverseHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 0 || neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 180 || neighborE.hatRot == 300)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
            if (neighborSE.hasReverseHat)
                {
                    if (neighborSE.hatRot == 0 || neighborSE.hatRot == 60 || neighborSE.hatRot == 120 || neighborSE.hatRot == 300)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }

                neighborLong = neighborSW.GetNeighbor(HexDirection.W);
                if (neighborLong.hasHat)
                {
                    if (neighborLong.hatRot == 120)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                else if (neighborLong.hasReverseHat)
                {
                    if (neighborLong.hatRot == 0 || neighborLong.hatRot == 300)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                neighborLongReverse1 = neighborW.GetNeighbor(HexDirection.W);
                if (neighborLongReverse1.hasReverseHat)
                {
                    if (neighborLongReverse1.hatRot == 60)
                    {
                        long1Valid = false;
                    }
                    else
                    {
                        long1Valid = true;
                    }
                }
                neighborLongReverse2 = neighborSW.GetNeighbor(HexDirection.SE);
                if (neighborLongReverse2.hasReverseHat)
                {
                    if (neighborLongReverse2.hatRot == 300)
                    {
                        long2Valid = false;
                    }
                    else
                    {
                        long2Valid = true;
                    }
                }
            }
            else if (this.CompareTag("Reverse Hat"))
            {
                if (neighborSW.hasHat)
                {
                    Debug.Log("SW Hit");
                    if (neighborSW.hatRot == 0 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240 || neighborSW.hatRot == 300)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
                if (neighborW.hasHat)
                {
                    Debug.Log("W Hit");
                    if (neighborW.hatRot == 0 || neighborW.hatRot == 60 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
                if (neighborNW.hasHat)
                {
                    Debug.Log("NW Hat");
                    if (neighborNW.hatRot == 300)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
                if (neighborNE.hasHat)
                {
                    Debug.Log("NE hat");
                    if (neighborNE.hatRot == 60)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
                if (neighborE.hasHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 120 || neighborE.hatRot == 180 || neighborE.hatRot == 240 || neighborE.hatRot == 300)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
                if (neighborSE.hasHat)
                {
                    Debug.Log("SE Hat");
                    if (neighborSE.hatRot == 60 || neighborSE.hatRot == 120 || neighborSE.hatRot == 180 || neighborSE.hatRot == 240 || neighborSE.hatRot == 300)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }



                if (neighborSW.hasReverseHat)
                {
                    Debug.Log("SW Hat");
                    if (neighborSW.hatRot == 0 || neighborSW.hatRot == 60 || neighborSW.hatRot == 120 || neighborSW.hatRot == 180 || neighborSW.hatRot == 240)
                    {
                        SWValid = true;
                    }
                    else
                    {
                        SWValid = false;
                    }
                }
                if (neighborW.hasReverseHat)
                {
                    Debug.Log("W Hat");
                    if (neighborW.hatRot == 120 || neighborW.hatRot == 180 || neighborW.hatRot == 240 || neighborW.hatRot == 300)
                    {
                        WValid = true;
                    }
                    else
                    {
                        WValid = false;
                    }
                }
                if (neighborNW.hasReverseHat)
                {
                    Debug.Log("NW hat");
                    if (neighborNW.hatRot == 240)
                    {
                        NWValid = true;
                    }
                    else
                    {
                        NWValid = false;
                    }
                }
                if (neighborNE.hasReverseHat)
                {
                    Debug.Log("NE Hat");
                    if (neighborNE.hatRot == 0)
                    {
                        NEValid = true;
                    }
                    else
                    {
                        NEValid = false;
                    }
                }
                if (neighborE.hasReverseHat)
                {
                    Debug.Log("E Hat");
                    if (neighborE.hatRot == 0 || neighborE.hatRot == 60 || neighborE.hatRot == 120 || neighborE.hatRot == 300)
                    {
                        EValid = true;
                    }
                    else
                    {
                        EValid = false;
                    }
                }
                if (neighborSE.hasReverseHat)
                {
                    Debug.Log("SE hat");
                    if (neighborSE.hatRot == 0 || neighborSE.hatRot == 60 || neighborSE.hatRot == 120 || neighborSE.hatRot == 180 || neighborSE.hatRot == 240)
                    {
                        SEValid = true;
                    }
                    else
                    {
                        SEValid = false;
                    }
                }
                
                neighborLong = neighborNE.GetNeighbor(HexDirection.NW);
                if (neighborLong.hasReverseHat)
                {
                    if (neighborLong.hatRot == 120)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                else if (neighborLong.hasHat)
                {
                    if (neighborLong.hatRot == 240 || neighborLong.hatRot == 300)
                    {
                        longValid = false;
                    }
                    else
                    {
                        longValid = true;
                    }
                }
                neighborLongReverse1 = neighborE.GetNeighbor(HexDirection.NE);
                if ( neighborLongReverse1.hasHat)
                {
                    if (neighborLongReverse1.hatRot == 300)
                    {
                        long1Valid = false;
                    }
                    else
                    {
                        long1Valid = true;
                    }
                }
                neighborLongReverse2 = neighborNW.GetNeighbor(HexDirection.NW);
                if (neighborLongReverse2.hasHat)
                {
                    if(neighborLongReverse2.hatRot == 180)
                    {
                        long2Valid = false;
                    }
                    else
                    {
                        long2Valid = true;
                    }
                }
            }


        }


        Debug.Log("SWValid: " + SWValid);
        Debug.Log("WValid: " + WValid);
        Debug.Log("NWValid: " + NWValid);
        Debug.Log("NEValid: " + NEValid);
        Debug.Log("EValid: " + EValid);
        Debug.Log("SEValid: " + SEValid);
        Debug.Log("LongValid: " + longValid);
        Debug.Log("LongValid1: " + long1Valid);
        Debug.Log("LongValid2: " + long2Valid);

        if (SWValid && WValid && NWValid && NEValid && EValid && SEValid && longValid && long1Valid && long2Valid)
        {
            isValid = true;
        }
        else
        {
            isValid = false;
            SWValid = true;
            WValid = true; 
            NWValid = true; 
            NEValid = true; 
            EValid = true;
            SEValid = true;
            longValid = true;
            long1Valid = true;
            long2Valid = true;
        }

    }

    private IEnumerator CheckValid(float waveTime)
    {
        while (!gameManager.gameOver)
        {
            yield return new WaitForSeconds(waveTime / 5);

            IsPlacementValid();
        }
    }

}
