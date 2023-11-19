using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selecter : MonoBehaviour
{

    private GameManager gameManager;
    private HexGrid hexGrid;

    private GameObject HatTab;
    public List<Button> buttons = new List<Button>();

    // Update is called once per frame
    void Start()
    {
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        HatTab = GameObject.Find("HatTab");
        foreach (Button button in HatTab.transform.GetChild(0).GetChild(1).GetComponentsInChildren<Button>())
        {
            buttons.Add(button);
        }
    }

    private void OnMouseDown()
    {
        GameObject hat = transform.root.gameObject;
        if (!gameManager.tileSelected)
        {
            Debug.Log("Select");
            gameManager.tileSelected = true;

            if (hat.GetComponent<HatPlacer>())
            {
                HexCell landCell = hexGrid.GetCell(transform.position);
                landCell.hasHat = false;
                landCell.hasReverseHat = false;
                landCell.hatRot = 0;
                landCell.hatRotInt = 0;
                landCell.hatAbove = null;
            }
            else if (hat.GetComponent<MultiHatPlacer>())
            {
                foreach (ChecksValid check in hat.GetComponent<MultiHatPlacer>().validHats)
                {
                    HexCell landCell = hexGrid.GetCell(check.transform.position);
                    landCell.hasHat = false;
                    landCell.hasReverseHat = false;
                    landCell.hatRot = 0;
                    landCell.hatRotInt = 0;
                    landCell.hatAbove = null;
                }
            }
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].interactable = false;
            }
            if (gameManager.GetComponent<TilingHoleMaker>())
            {
                if (this.transform.root.CompareTag("Hat"))
                {
                    gameManager.GetComponent<TilingHoleMaker>().hats += 1;
                }
                else if (this.transform.root.CompareTag("Reverse Hat"))
                {
                    gameManager.GetComponent<TilingHoleMaker>().reverseHats += 1;
                }
            }
            StartCoroutine(TrueSelecta(hat));
            gameManager.selectedTile = hat.gameObject;
        }
    }

    IEnumerator TrueSelecta(GameObject hat)
    {
        yield return new WaitForSeconds(0.05f);
        if (hat.GetComponent<HatPlacer>())
        {
            hat.GetComponent<HatPlacer>().isSelected = true;
        }
        if (hat.GetComponent<MultiHatPlacer>())
        {
            hat.GetComponent<MultiHatPlacer>().isSelected = true;
        }
    }
}
