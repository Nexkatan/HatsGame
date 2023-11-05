using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public List<GameObject> touchingHats = new List<GameObject>();
    public bool isConnected;

    private void OnCollisionStay(Collision collision)
    {
        isConnected = true;
        if (!touchingHats.Contains(collision.transform.root.gameObject))
        {
            touchingHats.Add(collision.transform.root.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isConnected = false;
        touchingHats.Remove(collision.transform.root.gameObject);
    }

    
}
