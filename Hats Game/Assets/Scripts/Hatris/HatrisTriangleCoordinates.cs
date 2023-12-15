using UnityEngine;

[System.Serializable]
public struct HatrisTriangleCoordinates
{
    [SerializeField]
    private int x;

    public int X
    {
        get
        {
            return x;
        }
    }



    public HatrisTriangleCoordinates(int x)
    {
        this.x = x;
    }

    public static HatrisTriangleCoordinates FromOffsetCoordinates(int x)
    {
        return new HatrisTriangleCoordinates(x);
    }

  
    public override string ToString()
    {
        return "(" + X.ToString();
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString();
    }

    public static HatrisTriangleCoordinates FromCell(HatrisHexCell cell)
    {

        

        int iX = Mathf.RoundToInt(UnityEditor.TransformUtils.GetInspectorRotation(cell.transform).y);

        return new HatrisTriangleCoordinates(iX);
    }
}

