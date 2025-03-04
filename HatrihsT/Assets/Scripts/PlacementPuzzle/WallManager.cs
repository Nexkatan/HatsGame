using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject[] wallPrefabs;
    public float timeBetweenWaves;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timeBetweenWaves = gameManager.difficulty;
        if (wallPrefabs[0] != null)
        {
            SpawnWall();
        }
    }

    public void SpawnWall()
    {
        if (!gameManager.gameOver)
        {
            GameObject wall = wallPrefabs[Random.Range(0, wallPrefabs.Length)];
            Instantiate(wall, wall.transform.position, wall.transform.rotation);
            StartCoroutine(SpawnSpeed((1 / timeBetweenWaves) * 16));
        }
    }
    IEnumerator SpawnSpeed(float waveTime) 
    {
        yield return new WaitForSeconds(waveTime);
        SpawnWall();
    }
    
}
