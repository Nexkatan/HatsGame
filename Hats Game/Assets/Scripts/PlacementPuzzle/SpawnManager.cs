using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{

    public GameObject hat;
    public bool isSelected;
    public bool isPlaced;
    private Vector3 buttonPos;

    private GameManager gameManager;

    private GameObject HatTab;
    private List<Button> buttons = new List<Button>();


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        buttonPos = transform.position;
    }

    private void Start()
    {
        HatTab = GameObject.Find("HatTab");
        if (HatTab)
        {
            foreach (Button button in HatTab.transform.GetChild(0).GetChild(1).GetComponentsInChildren<Button>())
            {
                buttons.Add(button);
            }
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
            Debug.Log("SPAWN");
            Vector3 mousePos = Input.mousePosition;
            GameObject hatObj = Instantiate(hat, Vector3.zero, hat.transform.rotation);

            Debug.Log(hatObj.name);
            if (this.CompareTag("Hat") || this.CompareTag("Reverse Hat"))
            {
                hatObj.gameObject.GetComponent<HatPlacer>().isSelected = true;
            }
            else if ((this.CompareTag("Multi Hat"))) 
            {
                hatObj.gameObject.GetComponent<MultiHatPlacer>().isSelected = true;
            }
            gameManager.tileSelected = true;
            gameManager.selectedTile = hatObj.gameObject;

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].interactable = false;
            }
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
    
}
