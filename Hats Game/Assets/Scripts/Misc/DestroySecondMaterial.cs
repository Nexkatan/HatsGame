using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestroySecondMaterial : MonoBehaviour
{
    private Material[] mat =  new Material[1];
    public Material pink;
    public Material pinkDarker;
    public Material pinkDarkest;
    public Material purple;
    public Material blueLight;
    public Material greenLight;
    public Material blueDark;
    public Material greenDark;
    public Material yellow;
    void Start()
    {
        
        Debug.Log("mat length: " + mat.Length);
        DestroyHatSecondMat();
    }
    public void DestroyHatSecondMat()
    {
        Material redMaterial = Resources.Load("White_mat", typeof(Material)) as Material;
        foreach (Transform child in transform)
        {
            if (child.GetChild(0).GetComponentInChildren<MeshRenderer>().materials.Length > 1)
            {
                mat[0] = child.GetChild(0).GetComponentInChildren<MeshRenderer>().materials[0];
                child.GetChild(0).GetComponentInChildren<MeshRenderer>().materials = mat;
                //Debug.Log(mat[0].ToString());
                if (mat[0].ToString() == "Pink_DarkerStill_mat (Instance) (UnityEngine.Material)")
                {
                    Debug.Log("Changed1");
                    mat[0] = redMaterial;
                }
                
                    
            }
           
        }
    }
}
