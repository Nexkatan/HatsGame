using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChain : MonoBehaviour
{
    public bool isConnected;
    public List<GameObject> connectedHats = new List<GameObject>();

    private void OnCollisionStay(Collision collision)
    {
        isConnected = true;
        if (!connectedHats.Contains(collision.transform.parent.parent.gameObject))
        {
            connectedHats.Add(collision.transform.parent.parent.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isConnected = false;
        connectedHats.Clear();
    }
}
