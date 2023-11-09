using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool tileSelected;
    public GameObject selectedTile;
    public bool gameOver = false;
    public int difficulty = 2;

   public void LoadHome()
    {
        SceneManager.LoadScene("MainScreen");
    }
   public void LoadHoleInTheWall()
    {
        SceneManager.LoadScene("Hole In The Wall");
    }
    public void LoadLevelEditor()
    {
        SceneManager.LoadScene("Level Editor");
    }
    public void LoadPlacementPuzzle()
    {
        SceneManager.LoadScene("Placement Puzzle Level Selector");
    }

   
}
