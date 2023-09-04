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
    private void Start()
    {
        wallMover = this.GetComponentInParent<WallMover>();
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerHat = GameObject.Find("PlayerHat").GetComponent<HatController>();
        currentCell = hexGrid.GetCell(transform.position);
        SetHex();
        currentCell.hatRot = Mathf.Round(transform.eulerAngles.y);
        
    }

    void Update()
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

                moveTime = false;
            
                StartCoroutine(MoveDown());
                }

            }
            else
            {
                Debug.Log("Gameover");
                gameManager.gameOver = true;
            }
        }
        if (currentCell.coordinates.Z < 1)
        {
            Destroy(gameObject);
        }
    }

    public void SetHex()
    {
        Debug.Log("Set");
        if (this.CompareTag("Hat"))
        {
            currentCell.hasHat = true;
        }
        else if (this.CompareTag("Reverse Hat"))
        {
            currentCell.hasReverseHat = true;
        }
        currentCell.hatRot = Mathf.Round(transform.eulerAngles.y);
    }

    public void ResetHex()
    {
        Debug.Log("Reset");
        if (this.CompareTag("Hat"))
        {
            currentCell.hasHat = false;
        }
        else if (this.CompareTag("Reverse Hat"))
        {
            currentCell.hasReverseHat = false;
        }
    }
   IEnumerator MoveDown()
    {
            yield return new WaitForSeconds(.5f);
            moveTime = true;
    }
}
