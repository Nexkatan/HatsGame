using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallHat : MonoBehaviour
{
    private WallMover wallMover;
    public HexCell currentCell;
    public HatController playerHat;
    private GameManager gameManager;
    private HexGrid hexGrid;
    public HexCell moveCell;

    public bool moveTime;
    private bool alternate;

    private bool gameOver = false;
    private float difficulty;

    public bool isPlayer = false;
    public bool isWallMover = false;

    private void Start()
    {
        
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (isPlayer)
        {
            playerHat = GameObject.Find("PlayerHat").GetComponent<HatController>();
        }
        if (isWallMover)
        {
            wallMover = this.GetComponentInParent<WallMover>();
        }
        currentCell = hexGrid.GetCell(transform.position);
        SetHex();
        currentCell.hatRot = Mathf.Round(transform.eulerAngles.y);
        difficulty = gameManager.difficulty;
    }

    void Update()
    {
        if (isWallMover)
        {
            if (wallMover.isMoving && !gameManager.gameOver)
            {
                if (playerHat.isValid)
                {
                    currentCell = hexGrid.GetCell(transform.position);
                    if (moveTime)
                    {
                        ResetHex();
                        if (alternate)
                        {
                            moveCell = currentCell.GetNeighbor(HexDirection.SE);
                            currentCell = moveCell;
                            transform.position = currentCell.transform.position;
                            alternate = false;
                            SetHex();
                        }
                        else
                        {
                            moveCell = currentCell.GetNeighbor(HexDirection.SW);
                            currentCell = moveCell;
                            transform.position = currentCell.transform.position;
                            SetHex();
                            alternate = true;
                        }
                        StartCoroutine(MoveDown(1 / difficulty));
                        moveTime = false;
                    }

                }
                else
                {
                    Debug.Log("Gameover");
                    gameManager.gameOver = true;
                }
            }
        }
       
        if (currentCell.coordinates.Z < 1)
        {
            Destroy(gameObject);
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
    }

    public void ResetHex()
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
    }
   IEnumerator MoveDown(float downSpeed)
    {
            yield return new WaitForSeconds(downSpeed);
            moveTime = true;
    }
}
