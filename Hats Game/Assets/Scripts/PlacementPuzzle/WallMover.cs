using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class WallMover : MonoBehaviour
{
    public bool isMoving;

    private GameManager gameManager;
    private HexGrid hexGrid;

    private bool moveTime = true;

    private WallHat[] wallHats;
    private int randomHat;
    private float difficulty;


    private HatController player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<HatController>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        difficulty = gameManager.difficulty;

        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();

        List<GameObject> hats = new List<GameObject>(); 
        List<WallHat> wallHatsList = new List<WallHat>();

        foreach (Transform t in transform)
        { 
            hats.Add(t.gameObject);
            if (t.gameObject.GetComponent<WallHat>())
            {
                t.gameObject.GetComponent<WallHat>().currentCell = hexGrid.GetCell(transform.position);
                wallHatsList.Add(t.GetComponent<WallHat>());
            }
        }

        wallHats = new WallHat[wallHatsList.Count];
        for (int i = 0; i < wallHatsList.Count; i++)
        {
            wallHats[i] = wallHatsList[i];
        }

        randomHat = Random.Range(2, hats.ToArray().Length - 2);
        Destroy(wallHats[randomHat].gameObject);
        Destroy(wallHats[randomHat - 1].gameObject);
    }
    void Update()
    {
        Move();
        if (transform.childCount < 1)
        {
            Destroy(gameObject);
            player.score++;
            player.SetScore();
        }
    }
    IEnumerator MoveDown(float downSpeed)
    {
        yield return new WaitForSeconds(downSpeed);
        moveTime = true;
    }

    public void Move()
    {
        if (isMoving && !gameManager.gameOver)
        {
            if (moveTime)
            {
                for (int i = 0; i < randomHat-1; i++)
                {
                    moveTwoDirections(2, i);
                }
                for (int i = randomHat+1; i < wallHats.Length; i++)
                {
                    moveTwoDirections(2, i);
                }
                moveTime = false;
            }
        }
    }

    void moveTwoDirections(int direction, int i)
    {
        if (wallHats[i])
        {
            if (wallHats[i].alternate)
            {
                wallHats[i].moveCell = wallHats[i].currentCell.GetNeighbor((HexDirection)(direction));
            }
            else
            {
                wallHats[i].moveCell = wallHats[i].currentCell.GetNeighbor((HexDirection)((direction + 1) % 6));
            }
            wallHats[i].ResetHex(wallHats[i].currentCell);
            wallHats[i].currentCell = wallHats[i].moveCell;
            wallHats[i].transform.position = wallHats[i].currentCell.transform.position;
            wallHats[i].alternate = !wallHats[i].alternate;
            wallHats[i].SetHex();
            StartCoroutine(MoveDown(1 / difficulty));
        }
    }
}