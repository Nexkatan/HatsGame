using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject hat;
    public bool isSelected;
    public bool isPlaced;
    private Vector3 buttonPos;

    private GameManager gameManager;
    public GameObject wallPrefab;
    public float timeBetweenWaves;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        buttonPos = transform.position;
        timeBetweenWaves = gameManager.difficulty;
        if (wallPrefab != null)
        {
            SpawnWall();
        }
    }

   


    private void OnMouseDown()
    {
        SpawnHat();
    }


    public void SpawnHat()
    {
        if (!isSelected) 
        {
            Vector3 mousePos = Input.mousePosition;
            Instantiate(hat, mousePos, hat.transform.rotation);
            if (this.CompareTag("Hat") || this.CompareTag("Reverse Hat"))
            {
                hat.gameObject.GetComponent<HatPlacer>().isSelected = true;
            }
            else if ((this.CompareTag("Multi Hat"))) 
            {
                hat.gameObject.GetComponent<MultiHatPlacer>().isSelected = true;
            }
            gameManager.tileSelected = true;
            gameManager.selectedTile = hat.gameObject;
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
            //return hexGrid.ColorCell(hit.point);
        }
        return null;
    }

    public void SpawnWall()
    {
        if (!gameManager.gameOver)
        {
            GameObject wall = Instantiate(wallPrefab, wallPrefab.transform.position, wallPrefab.transform.rotation);
            StartCoroutine(SpawnSpeed((1 / timeBetweenWaves) * 16));
        }
    }
    IEnumerator SpawnSpeed(float waveTime) 
    {
        yield return new WaitForSeconds(waveTime);
        SpawnWall();
        
    }
    
}
