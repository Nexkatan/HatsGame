using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Avatar : MonoBehaviour
{
    private Player player;

    public float explosionRadius;

    private ParticleSystem burst;
    private float deathCountdown = -1f;

    public float rotFromStart;

    private void Awake()
    {
        player = transform.root.GetComponent<Player>();
        burst = player.burst;
    }

    private void Start()
    {
        rotFromStart = 120;
    }
    private void Update()
    {
        if (deathCountdown >= 0f)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            ParticleSystem.MainModule main = burst.main;
            var em = main.maxParticles;
            burst.Emit(em);
            player.acceleration = -(player.velocity * player.velocity);
            deathCountdown -= Time.deltaTime;
            if (deathCountdown <= 0f)
            {
                deathCountdown = -1f;
                player.Die();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        float wrap = Mathf.Round(rotFromStart + 360) % 360;
        float gateWrap = Mathf.Round(other.transform.parent.parent.parent.GetComponent<PipeItem>().gateRot + 360) % 360;
        float reverseGateWrap = Mathf.Round(other.transform.parent.parent.parent.GetComponent<PipeItem>().gateRot + 420) % 360;


        wrapNumbers(wrap);
        wrapNumbers(gateWrap);
        wrapNumbers(reverseGateWrap);
       

        if (other.transform.GetComponentInParent<PipeItem>().isReverse)
        {
            if (transform.parent.parent.CompareTag("Hat"))
            {
                Debug.Log("Not Same");
                Dying(other);
            }
            else
            {
                if (transform.GetChild(0).localScale.x < 0)
                {
                    if (Mathf.Abs(wrap - reverseGateWrap) > 1)
                    {
                        if (deathCountdown < 0)
                        {
                            Debug.Log("Not Safe Reverse");
                            Debug.Log("Wrap: " + wrap);
                            Debug.Log("reverse Gate: " + reverseGateWrap);
                            Dying(other);
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                    Dying(other);
                }

            }
            
            
        }
        else if (!other.transform.GetComponentInParent<PipeItem>().isReverse)
        {
            if (transform.parent.parent.CompareTag("Reverse Hat"))
            {
                Debug.Log("Not Same");
                Dying(other);
            }
            if (transform.GetChild(0).localScale.x > 0)
            {
                if (Mathf.Abs(wrap -gateWrap) > 1)
                {
                    if (deathCountdown < 0)
                    {
                        Debug.Log("Not Safe");
                        Debug.Log("Wrap: " + wrap);
                        Debug.Log("Gate: " + gateWrap);
                        Dying(other);
                    }
                }
                else
                {
                }
            }
            else
            {
                Dying(other);
            }
        }
       
    }

    float wrapNumbers(float wrapp)
    {

        if (wrapp == 360)
        {
            wrapp = 0;
        }
       
        if (wrapp == 180)
        {
            wrapp = -180;
        }
      return wrapp;
    }

    private void Dying(Collider other)
    {
        GameObject gateObj = other.transform.parent.parent.GetChild(0).gameObject;
        GameObject brokenGateObj = other.transform.parent.parent.GetChild(1).gameObject;

        Debug.Log(player.worldRot);

        gateObj.SetActive(false);
        brokenGateObj.SetActive(true);

        ParticleSystem.MainModule main = burst.main;
        deathCountdown = main.startLifetime.constant;
    }

}
