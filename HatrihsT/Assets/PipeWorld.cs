using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeWorld : MonoBehaviour
{
    // Start is called before the first frame update
    public float myRotation;

    private void FixedUpdate()
    {
        Vector3 eulers = transform.localEulerAngles;

        float W = eulers.x;
        float checkY = eulers.y;

        if (checkY == 180)
        {
            myRotation = 180 - W;
        }
        else
        {
            myRotation = W;
        }
    }
}
