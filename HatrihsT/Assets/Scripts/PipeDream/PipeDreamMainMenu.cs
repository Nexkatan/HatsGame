using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PipeDreamMainMenu : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI scoreLabel;
    public GameObject initialBG;
    public PipeSystem pipeSystem;

    private bool gameOver;

    public void StartGame(int mode)
    {
        if (!gameOver)
        {
            player.StartGame(mode);
            gameObject.SetActive(false);
            Cursor.visible = false;
            initialBG.SetActive(false);
        }
        else
        {
            Material pipeMat = player.pipeMat;
            Pipe pipe1 = pipeSystem.SetupFirstPipe();
            pipe1.GetComponent<MeshRenderer>().material = pipeMat;
            Pipe pipe2 = pipeSystem.SetupNextPipe();
            pipe2.GetComponent<MeshRenderer>().material = pipeMat;

            player.StartGame(mode);
            gameObject.SetActive(false);
            Cursor.visible = false;
            initialBG.SetActive(false);
        }
        
        player.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
        gameOver = false;

    }

    public void EndGame(float distanceTravelled)
    {
        gameOver = true;
        scoreLabel.text = ((int)distanceTravelled * 10f).ToString();
        gameObject.SetActive(true);
        Cursor.visible = true;
    }
}
