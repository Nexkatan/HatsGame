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
  
    public GameObject colliders;

    public bool isSelected;

    public HexCell currentCell;
    public HexCell landCell;

    private Vector3 thisHatRot;
    public int thisHatRotInt;

    private GameManager gameManager;

    private ChecksValid validityCheck;

    private float checkTime;

    public bool is3D;

    void Start()
    {
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        validityCheck = this.GetComponent<ChecksValid>();
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
        if (!is3D)
        {
            HexMove();
        }
        HexSpin();
    }


    void HexSpin()
    {
        if (!gameManager.gameOver)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.J))
            {
                Vector3 m_EulerAngleVelocity = new Vector3(0, 60, 0);
                Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity);
                thisHatRot += m_EulerAngleVelocity;
                thisHatRotInt += 1 % 6;
                Debug.Log(thisHatRot);

                validityCheck.IsPlacementValid(currentCell);

                if (validityCheck.isValid)
                {
                    this.transform.rotation *= deltaRotation;
                }
                else
                {
                    thisHatRot -= m_EulerAngleVelocity;
                }
            }
        }
    }
    void HexMove()
    {
        if (!gameManager.gameOver)
        {
            if (Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.W))
            {
                currentCell.hasHat = false;
                currentCell.hasReverseHat = false;
                landCell = currentCell.GetNeighbor(HexDirection.NW);

                if (!landCell.hasHat && !landCell.hasReverseHat)
                {
                    validityCheck.IsPlacementValid(landCell);

                    if (validityCheck.isValid)
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
                    validityCheck.IsPlacementValid(landCell);

                    if (validityCheck.isValid)
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
                    validityCheck.IsPlacementValid(landCell);

                    if (validityCheck.isValid)
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
                    validityCheck.IsPlacementValid(landCell);

                    if (validityCheck.isValid)
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
                    validityCheck.IsPlacementValid(landCell);

                    if (validityCheck.isValid)
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
                    validityCheck.IsPlacementValid(landCell);

                    if (validityCheck.isValid)
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
    }

    private IEnumerator CheckValid(float waveTime)
    {
        while (!gameManager.gameOver)
        {
            yield return new WaitForSeconds(waveTime / 5);

            validityCheck.IsPlacementValid(currentCell);
            if (!validityCheck.isValid)
            {
                gameManager.gameOver = true;
            }
        }
    }

}
