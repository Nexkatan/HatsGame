using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilingCut : MonoBehaviour
{
    public int widthStart;
    public int heightStart;
    public int widthEnd;
    public int heightEnd;
    private HexGrid hexGrid;

    void Start()
    {
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        StartCoroutine("CountDown");
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(.1f);
        DestroyHats();
    }

    void DestroyHats()
    {
        List<GameObject> hatsToDestroy = new List<GameObject>();
        
        for (int j = widthStart; j < widthEnd; j++) 
        {
            for (int i = heightStart; i < heightEnd; i++)
            {
                Vector3 cell = new Vector3(j, 0, i);
                if (hexGrid.GetCell(cell).hatAbove != null)
                {
                    hatsToDestroy.Add(hexGrid.GetCell(cell).hatAbove.gameObject);
                }
        }
        }
        

        Debug.Log(hatsToDestroy.Count);
        for (int i = 0; i < hatsToDestroy.Count; i++)
        {
            hatsToDestroy[i].transform.SetParent(this.transform.root);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
