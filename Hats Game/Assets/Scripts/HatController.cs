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
    public int thisHatRotInt;

    [SerializeField] HexCoordinates cellCo;
    
    public HexCell[] Neighbours;
    public HexCell[] Longbois;

    private int[][] cribsHatHat = { new int[] { 0, 3, 4, 5 }, new int[] { 5 }, new int[] { 1 }, new int[] { 0, 1, 2, 3 }, new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 } };
    private int[][] cribsHatReverseHat = { new int[] { 0, 1, 2, 3 }, new int[] { 4 }, new int[] { 0 }, new int[] { 0, 1, 4, 5 }, new int[] { 0, 1, 2, 5 }, new int[] { 0, 1, 2, 3, 4 } };
    private int[][] cribsReverseHatHat = { new int[] { 0, 2, 3, 4, 5 }, new int[] { 0, 1, 4, 5 }, new int[] { 0, 1, 2, 5 }, new int[] { 0 }, new int[] { 2 }, new int[] { 0, 3, 4, 5 } };
    private int[][] cribsReverseHatReverseHat = { new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 }, new int[] { 0, 3, 4, 5 }, new int[] { 5 }, new int[] { 1 }, new int[] { 0, 1, 2, 3 } };

    private int[][] cribsLongboisHatHat = { new int[] { }, new int[] { }, new int[] { }, new int[] { 3 }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { } };
    private int[][] cribsLongboisHatReverseHat = { new int[] { }, new int[] { 0 }, new int[] { }, new int[] { 0, 1 }, new int[] { 2 }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { } };
    private int[][] cribsLongboisReverseHatHat = { new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { 4 }, new int[] { 0, 5 }, new int[] { }, new int[] { 0 }, new int[] { }, new int[] { } };
    private int[][] cribsLongboisReverseHatReverseHat = { new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { 3 }, new int[] { }, new int[] { }, new int[] { }, new int[] { } };

    private bool[] NeighboursBool = { true, true, true, true, true, true };
    private bool[] LongboisBool = { true, true, true, true, true, true, true, true, true, true, true, true };

    public bool isValid = true;

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
        thisHatRot = transform.eulerAngles;
        thisHatRotInt = Mathf.RoundToInt(thisHatRot.y / 60) % 6;
    }

    private void Update()
    {
        HexMove();
        HexSpin();
    }


    void HexMove()
    {
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
                    landCell.hatRotInt = Mathf.RoundToInt(landCell.hatRot / 60) % 6;
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
            thisHatRotInt += 1 % 6;
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
        Neighbours[0] = landCell.GetNeighbor(HexDirection.SW);
        Neighbours[1] = landCell.GetNeighbor(HexDirection.W);
        Neighbours[2] = landCell.GetNeighbor(HexDirection.NW);
        Neighbours[3] = landCell.GetNeighbor(HexDirection.NE);
        Neighbours[4] = landCell.GetNeighbor(HexDirection.E);
        Neighbours[5] = landCell.GetNeighbor(HexDirection.SE);

        Longbois[0] = landCell.GetNeighbor(HexDirection.SW).GetNeighbor(HexDirection.SW);
        Longbois[1] = landCell.GetNeighbor(HexDirection.SW).GetNeighbor(HexDirection.W);
        Longbois[2] = landCell.GetNeighbor(HexDirection.W).GetNeighbor(HexDirection.W);
        Longbois[3] = landCell.GetNeighbor(HexDirection.W).GetNeighbor(HexDirection.NW);
        Longbois[4] = landCell.GetNeighbor(HexDirection.NW).GetNeighbor(HexDirection.NW);
        Longbois[5] = landCell.GetNeighbor(HexDirection.NW).GetNeighbor(HexDirection.NE);
        Longbois[6] = landCell.GetNeighbor(HexDirection.NE).GetNeighbor(HexDirection.NE);
        Longbois[7] = landCell.GetNeighbor(HexDirection.NE).GetNeighbor(HexDirection.E);
        Longbois[8] = landCell.GetNeighbor(HexDirection.E).GetNeighbor(HexDirection.E);
        Longbois[9] = landCell.GetNeighbor(HexDirection.E).GetNeighbor(HexDirection.SE);
        Longbois[10] = landCell.GetNeighbor(HexDirection.SE).GetNeighbor(HexDirection.SE);
        Longbois[11] = landCell.GetNeighbor(HexDirection.SE).GetNeighbor(HexDirection.SW);

        for (int j = 0; j < NeighboursBool.Length; j++)
        {
            NeighboursBool[j] = true;
            LongboisBool[j] = true;
            LongboisBool[j + 6] = true;
        }

        if (this.CompareTag("Hat"))
        {
            for (int i = 0; i < Neighbours.Length; i++)
            {
                if (Neighbours[(i + thisHatRotInt) % 6].hasHat)
                {
                    int calc = ((Neighbours[(i + thisHatRotInt) % 6].hatRotInt + 6 - thisHatRotInt) % 6);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsHatHat[i])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count == 0)
                    {
                        NeighboursBool[i] = false;
                    }
                }
                else if (Neighbours[(i + thisHatRotInt) % 6].hasReverseHat)
                {
                    int calc = ((Neighbours[(i + thisHatRotInt) % 6].hatRotInt + 6 - thisHatRotInt) % 6);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsHatReverseHat[i])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count == 0)
                    {
                        NeighboursBool[i] = false;
                    }
                }
            }
            for (int j = 0; j < Longbois.Length; j++)
            {
                if (Longbois[(j + thisHatRotInt) % 12].hasHat)
                {
                    int calc = ((Longbois[(j + thisHatRotInt) % 12].hatRotInt + 12 - thisHatRotInt) % 6);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsLongboisHatHat[(j + 12 - thisHatRotInt) % 12])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count > 0)
                    {
                        LongboisBool[j] = false;
                    }
                }
                else if (Longbois[(j + thisHatRotInt) % 12].hasReverseHat)
                {
                    int calc = ((Longbois[(j + thisHatRotInt) % 12].hatRotInt + 12 - thisHatRotInt) % 6);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsLongboisHatReverseHat[(j + 12 - thisHatRotInt) % 12])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count > 0)
                    {
                        LongboisBool[j] = false;
                    }
                }
            }
        }
        else if (this.CompareTag("Reverse Hat"))
        {
            for (int i = 0; i < Neighbours.Length; i++)
            {
                if (Neighbours[(i + thisHatRotInt) % 6].hasHat)
                {
                    int calc = ((Neighbours[(i + thisHatRotInt) % 6].hatRotInt + 6 - thisHatRotInt) % 6);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsReverseHatHat[i])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count == 0)
                    {
                        NeighboursBool[i] = false;
                    }
                }
                else if (Neighbours[(i + thisHatRotInt) % 6].hasReverseHat)
                {
                    int calc = ((Neighbours[(i + thisHatRotInt) % 6].hatRotInt + 6 - thisHatRotInt) % 6);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsReverseHatReverseHat[i])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count == 0)
                    {
                        NeighboursBool[i] = false;
                    }
                }
            }
            for (int j = 0; j < Longbois.Length; j++)
            {
                if (Longbois[(j + thisHatRotInt) % 12].hasHat)
                {
                    int calc = ((Longbois[(j + thisHatRotInt) % 12].hatRotInt + 12 - thisHatRotInt) % 6);
                    Debug.Log("calc = " + calc);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsLongboisReverseHatHat[(j + 12 - thisHatRotInt) % 12])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count > 0)
                    {
                        LongboisBool[j] = false;
                    }
                }
                else if (Longbois[(j + thisHatRotInt) % 12].hasReverseHat)
                {
                    int calc = ((Longbois[(j + thisHatRotInt) % 12].hatRotInt + 12 - thisHatRotInt) % 6);
                    Debug.Log("calc = " + calc);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsLongboisReverseHatReverseHat[(j + 12 - thisHatRotInt) % 12])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count > 0)
                    {
                        LongboisBool[j] = false;
                    }
                }
            }
        }
        if (AllNeighboursValid())
        {
            isValid = true;
        }
        else
        {
            isValid = false;
        }
    }

    public bool AllNeighboursValid()
    {
        for (int i = 0; i < NeighboursBool.Length; i++)
            if (!NeighboursBool[i])
            {
                Debug.Log(((HexDirection)((i + thisHatRotInt) % 6)).Opposite());
                return false;
            }
        for (int j = 0; j < LongboisBool.Length; j++)
            if (!LongboisBool[j])
            {
                Debug.Log(((LongboiDirection)((j + thisHatRotInt) % 12)));
                Debug.Log(thisHatRotInt);
                return false;
            }
        return true;
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
