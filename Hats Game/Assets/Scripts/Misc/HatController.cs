using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UIElements;

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

    public Material mat1;
    public Material mat2;
    public bool matBool;

    private String normalTag = "Hat";
    private String reverseTag = "Reverse Hat";

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
        HatMove();
        HatSpin();
        FlipHat();
    }


    void HatSpin()
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
    void HatMove()
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

    private void FlipHat()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 m_EulerAngleVelocityPos = new Vector3(0, 60, 0);
            Vector3 m_EulerAngleVelocityNeg = new Vector3(0, -60, 0);
            Quaternion deltaRotationPos = Quaternion.Euler(m_EulerAngleVelocityPos);
            Quaternion deltaRotationNeg = Quaternion.Euler(m_EulerAngleVelocityNeg);

            if (transform.localScale.x < 0)
            {
                transform.rotation *= deltaRotationNeg;
            }
            else
            {
                transform.rotation *= deltaRotationPos;
            }

            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            if (matBool)
            {
                transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = mat2;
                matBool = !matBool;
                ResetHex(currentCell);
                tag = reverseTag;
                SetHex(currentCell);

            }
            else
            {
                transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = mat1;
                matBool = !matBool;
                ResetHex(currentCell);
                tag = normalTag;
                SetHex(currentCell);
            }

        }
    }

    public void SetHex(HexCell currentCell)
    {
        if (this.CompareTag("Hat"))
        {
            currentCell.hasHat = true;
        }
        else if (this.CompareTag("Reverse Hat"))
        {
            currentCell.hasReverseHat = true;
        }
        currentCell.hatRotInt = thisHatRotInt;
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
        currentCell.hatRotInt = 0;
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
