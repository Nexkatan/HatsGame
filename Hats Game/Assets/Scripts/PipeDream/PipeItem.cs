using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeItem : MonoBehaviour
{
    private Transform rotater;
    
    private int count = 0;
    private Player player;

    public bool staticGateRot;
    public bool isReverse;
    private void Awake()
    {
        rotater = transform.GetChild(0);
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (staticGateRot)
        {

            rotater.localRotation = Quaternion.Euler(transform.parent.gameObject.GetComponent<Pipe>().gateRot - player.worldRot, 0, 0);
        }
    }


    public void Position(Pipe pipe, float curveRotation, float ringRotation)
    {
        transform.SetParent(pipe.transform, false);
        transform.localRotation = Quaternion.Euler(0f, 0f, -curveRotation);
        rotater.localPosition = new Vector3(0f, pipe.CurveRadius);
        rotater.localRotation = Quaternion.Euler(transform.parent.gameObject.GetComponent<Pipe>().gateRot - player.worldRot, 0, 0);
    }
}
