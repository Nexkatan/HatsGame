using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI velocityLabel, distanceLabel;
    
    public void SetValues(float distanceTraveled, float velocity)
    {
        velocityLabel.text = ((int)(velocity * 10f)).ToString();
        distanceLabel.text = ((int)(distanceTraveled * 10f)).ToString();
    }
}
