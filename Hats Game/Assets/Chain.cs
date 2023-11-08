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
        if (!touchingHats.Contains(collision.transform.parent.parent.gameObject))
        {
            touchingHats.Add(collision.transform.parent.parent.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isConnected = false;
        touchingHats.Remove(collision.transform.parent.parent.gameObject);
    }

    
}
