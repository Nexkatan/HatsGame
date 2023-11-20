using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinButton : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


  void deleteButton()
    {
        if (gameManager.tileSelected)
        {
            Destroy(gameManager.selectedTile);
        }
    }
}
