using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PipeSystem pipeSystem;

    public float startVelocity, rotationVelocity;

    public float[] accelerations;

    public float acceleration, velocity;

    public Pipe currentPipe;

    private float distanceTraveled;

    private float deltaToRotation, systemRotation;

    private Transform world, rotater;
    private float worldRotation, avatarRotation;

    public float worldRot;

    private Camera mainCam;

    public ParticleSystem burst;
    
    private bool matBool;
    public Material mat1;
    public Material mat2;

    private bool isActive;

    public PipeDreamMainMenu mainMenu;
    public HUD hud;

    private void Awake()
    {
        world = pipeSystem.transform.parent;
        rotater = transform.GetChild(0);
        mainCam  = GameObject.Find("Main Camera").GetComponent<Camera>();
        isActive = false;
    }

    public void StartGame(int accelerationMode)
    {
        distanceTraveled = 0f;
        avatarRotation = 0f;
        systemRotation = 0f;
        worldRotation = 0f;
        currentPipe = pipeSystem.SetupFirstPipe();
        deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
        acceleration = accelerations[accelerationMode];
        velocity = startVelocity;
        SetupCurrentPipe();
        isActive = true;

        hud.gameObject.SetActive(true);
        hud.SetValues(distanceTraveled, velocity);
    }

    private void Update()
    {
        velocity += acceleration * Time.deltaTime;
        float delta = velocity * Time.deltaTime;
        distanceTraveled += delta;
        systemRotation += delta * deltaToRotation;
        if (isActive)
        {
            if (systemRotation >= currentPipe.CurveAngle)
            {
                delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
                currentPipe = pipeSystem.SetupNextPipe();
                SetupCurrentPipe();
                systemRotation = delta * deltaToRotation;
            }

            pipeSystem.transform.localRotation =
               Quaternion.Euler(0f, 0f, systemRotation);
            FlipHat();
            UpdateAvatarRotation();

            worldRot = UnityEditor.TransformUtils.GetInspectorRotation(world.transform).x;
        }
        hud.SetValues(distanceTraveled, velocity);
    }

    private void SetupCurrentPipe()
    {
        deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
        worldRotation += currentPipe.RelativeRotation;
        if (worldRotation < 0f)
        {
            worldRotation += 360f;
        }
        else if (worldRotation >= 360f)
        {
            worldRotation -= 360f;
        }
        world.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);
    }

    private void UpdateAvatarRotation()
    {
        avatarRotation +=
            rotationVelocity * Time.deltaTime * Input.GetAxis("Horizontal");
        if (avatarRotation < 0f)
        {
            avatarRotation += 360f;
        }
        else if (avatarRotation >= 360f)
        {
            avatarRotation -= 360f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 m_EulerAngleVelocity = new Vector3(-60, 0, 0);
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity);
            rotater.GetChild(0).rotation *= deltaRotation;
            rotater.localRotation = Quaternion.Euler(avatarRotation, 0f, 0f);
        }
    }

    private void FlipHat()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transform playerHat = rotater.GetChild(0).GetChild(0);
            Vector3 m_EulerAngleVelocityPos = new Vector3(0, 60, 0);
            Vector3 m_EulerAngleVelocityNeg = new Vector3(0, -60, 0);
            Quaternion deltaRotationPos = Quaternion.Euler(m_EulerAngleVelocityPos);
            Quaternion deltaRotationNeg = Quaternion.Euler(m_EulerAngleVelocityNeg);

            if (playerHat.localScale.x < 0)
            {
                playerHat.rotation *= deltaRotationNeg;
            }
            else
            {
                playerHat.rotation *= deltaRotationPos;
            }
            playerHat.localScale = new Vector3(-playerHat.localScale.x, 0.175f, 0.175f);
            
           if (matBool)
            {
                burst.GetComponent<ParticleSystemRenderer>().material = mat2;
                playerHat.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = mat2;
                matBool = !matBool;
            }
           else
            {
                burst.GetComponent<ParticleSystemRenderer>().material = mat1;
                playerHat.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = mat1;
                matBool = !matBool;
            }
            
        }
    }

    public void Die()
    {
        Debug.Log("dead");
        gameObject.SetActive(false);
        
        hud.gameObject.SetActive(false);

        mainMenu.EndGame(distanceTraveled);
    }
}
