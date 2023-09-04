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


    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        buttonPos = transform.position;
        SpawnWall();
    }

   


    private void OnMouseDown()
    {
        SpawnHat();
    }


    public void SpawnHat()
    {
        if (!isSelected) 
        {
            Instantiate(hat, buttonPos, hat.transform.rotation);
            hat.gameObject.GetComponent<HatController>().isSelected = true;
            gameManager.tileSelected = true;
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
            StartCoroutine(SpawnSpeed());
        }
    }
    IEnumerator SpawnSpeed() 
    {
        yield return new WaitForSeconds(8);
        SpawnWall();
        
    }
    
}
