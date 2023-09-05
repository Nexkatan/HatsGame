using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMover : MonoBehaviour
{
    public bool isMoving;

    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        List<GameObject> hats = new List<GameObject>();
        foreach (Transform t in transform)
        { 
                hats.Add(t.gameObject);
        }
        

        int randomHat = Random.Range(2, hats.ToArray().Length-2);

        Destroy(hats[randomHat].gameObject);
        Destroy(hats[randomHat-1].gameObject);
    }
}