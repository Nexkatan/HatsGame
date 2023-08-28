using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class HatController : MonoBehaviour
{
    public Color[] colors;

    private HexGrid hexGrid;
    private Rigidbody rb;

    private Vector3 mousePos;
    private Camera screenCamera;

    public GameObject colliders;

    public bool isSelected;

    public int hatHits = 0;

    void Start()
    {
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        screenCamera = Camera.main;
        //rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isSelected)
        { 
            Move();
        }
        else
        {

        }

    }

    private void Update()
    {
        if (isSelected)
        {
            Spin();
        }
        if (Input.GetMouseButtonDown(0))
            if (isSelected)
            {
                Deselect();
            }
    }

    void Move()
    {
            mousePos = Input.mousePosition;

        if (GetCellUnderCursor() != null)
        {

            HexCell currentCell = GetCellUnderCursor();
            HexCell stayCell = GetCellUnderHat();
            if (isSelected)
            {
                this.transform.position = (currentCell.transform.position);
            }
            else
            {
                this.transform.position = (stayCell.transform.position);
            }
        }   
    }

    void Spin()
    {
        if (Input.GetMouseButtonDown((1)))
        {
            Vector3 m_EulerAngleVelocity = new Vector3(0, 60, 0);
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity);
            this.transform.rotation *= deltaRotation;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("select");
        StartCoroutine(TrueSelecta());
        Destroy(this.GetComponent<Rigidbody>());
    }

    private void Deselect()
    {
        Debug.Log("deselected at: " + GetCellUnderHat().coordinates);
        isSelected = false;
        this.AddComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Hat"))
        {
            Debug.Log("TriggerHit");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hat"))
        {
            Debug.Log("CollisionHit");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Hat"))
        {

            Debug.Log("DeHit");
        }
    }


    HexCell GetCellUnderCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 200;
        Ray inputRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            return hexGrid.GetCell(hit.point);
        }
        return null;
    }

    HexCell GetCellUnderHat()
    {
        Vector3 hatPos = transform.position;
        HexCell hexCell = hexGrid.GetCell(hatPos);
        return hexGrid.GetCell(hatPos);
    }

    IEnumerator TrueSelecta()
    {
        yield return new WaitForSeconds(0.05f);
        isSelected = true;
    }
}
