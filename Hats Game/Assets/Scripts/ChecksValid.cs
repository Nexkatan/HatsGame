using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecksValid : MonoBehaviour
{
    public HexCell[] Neighbours;
    public HexCell[] Longbois;

    private int[][] cribsHatHat = { new int[] { 0, 3, 4, 5 }, new int[] { 5 }, new int[] { 1 }, new int[] { 0, 1, 2, 3 }, new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 } };
    private int[][] cribsHatReverseHat = { new int[] { 0, 1, 2, 3 }, new int[] { 4 }, new int[] { 0 }, new int[] { 0, 1, 4, 5 }, new int[] { 0, 1, 2, 5 }, new int[] { 0, 1, 2, 3, 4 } };
    private int[][] cribsReverseHatHat = { new int[] { 0, 2, 3, 4, 5 }, new int[] { 0, 1, 4, 5 }, new int[] { 0, 1, 2, 5 }, new int[] { 0 }, new int[] { 2 }, new int[] { 0, 3, 4, 5 } };
    private int[][] cribsReverseHatReverseHat = { new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 }, new int[] { 0, 3, 4, 5 }, new int[] { 5 }, new int[] { 1 }, new int[] { 0, 1, 2, 3 } };

    private int[][] cribsLongboisHatHat = { new int[] { }, new int[] { }, new int[] { }, new int[] { 3 }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { } };
    private int[][] cribsLongboisHatReverseHat = { new int[] { }, new int[] { 0 }, new int[] { }, new int[] { 0, 1 }, new int[] { 2 }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { } };
    private int[][] cribsLongboisReverseHatHat = { new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { 4 }, new int[] { 0, 5 }, new int[] { }, new int[] { 0 }, new int[] { }, new int[] { } };
    private int[][] cribsLongboisReverseHatReverseHat = { new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[] { 3 }, new int[] { }, new int[] { }, new int[] { }, new int[] { } };

    private bool[] NeighboursBool = { true, true, true, true, true, true };
    private bool[] LongboisBool = { true, true, true, true, true, true, true, true, true, true, true, true };

    public bool isValid = true;

    public HexCell currentCell;

    public float thisHatRot;
    public int thisHatRotInt;


    private HexGrid hexGrid;

    private void Start()
    {
        Neighbours = new HexCell[6];
        Longbois = new HexCell[12];
    }

    public void IsPlacementValid(HexCell landCell)
    {
        thisHatRot = Mathf.Round(transform.eulerAngles.y);
        thisHatRotInt = Mathf.RoundToInt(thisHatRot / 60) % 6;

        Neighbours[0] = landCell.GetNeighbor(HexDirection.SW);
        Neighbours[1] = landCell.GetNeighbor(HexDirection.W);
        Neighbours[2] = landCell.GetNeighbor(HexDirection.NW);
        Neighbours[3] = landCell.GetNeighbor(HexDirection.NE);
        Neighbours[4] = landCell.GetNeighbor(HexDirection.E);
        Neighbours[5] = landCell.GetNeighbor(HexDirection.SE);

        Longbois[0] = landCell.GetNeighbor(HexDirection.SW).GetNeighbor(HexDirection.SW);
        Longbois[1] = landCell.GetNeighbor(HexDirection.SW).GetNeighbor(HexDirection.W);
        Longbois[2] = landCell.GetNeighbor(HexDirection.W).GetNeighbor(HexDirection.W);
        Longbois[3] = landCell.GetNeighbor(HexDirection.W).GetNeighbor(HexDirection.NW);
        Longbois[4] = landCell.GetNeighbor(HexDirection.NW).GetNeighbor(HexDirection.NW);
        Longbois[5] = landCell.GetNeighbor(HexDirection.NW).GetNeighbor(HexDirection.NE);
        Longbois[6] = landCell.GetNeighbor(HexDirection.NE).GetNeighbor(HexDirection.NE);
        Longbois[7] = landCell.GetNeighbor(HexDirection.NE).GetNeighbor(HexDirection.E);
        Longbois[8] = landCell.GetNeighbor(HexDirection.E).GetNeighbor(HexDirection.E);
        Longbois[9] = landCell.GetNeighbor(HexDirection.E).GetNeighbor(HexDirection.SE);
        Longbois[10] = landCell.GetNeighbor(HexDirection.SE).GetNeighbor(HexDirection.SE);
        Longbois[11] = landCell.GetNeighbor(HexDirection.SE).GetNeighbor(HexDirection.SW);

        for (int j = 0; j < NeighboursBool.Length; j++)
        {
            NeighboursBool[j] = true;
            LongboisBool[j] = true;
            LongboisBool[j + 6] = true;
        }

        if (this.CompareTag("Hat"))
        {
            for (int i = 0; i < Neighbours.Length; i++)
            {
                if (Neighbours[(i + thisHatRotInt) % 6].hasHat)
                {
                    int calc = ((Neighbours[(i + thisHatRotInt) % 6].hatRotInt + 6 - thisHatRotInt) % 6);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsHatHat[i])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count == 0)
                    {
                        NeighboursBool[i] = false;
                    }
                }
                else if (Neighbours[(i + thisHatRotInt) % 6].hasReverseHat)
                {
                    int calc = ((Neighbours[(i + thisHatRotInt) % 6].hatRotInt + 6 - thisHatRotInt) % 6);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsHatReverseHat[i])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count == 0)
                    {
                        NeighboursBool[i] = false;
                    }
                }
            }
            for (int j = 0; j < Longbois.Length; j++)
            {
                if (Longbois[(j + thisHatRotInt) % 12].hasHat)
                {
                    int calc = ((Longbois[(j + thisHatRotInt) % 12].hatRotInt + 12 - thisHatRotInt) % 6);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsLongboisHatHat[(j + 12 - thisHatRotInt) % 12])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count > 0)
                    {
                        LongboisBool[j] = false;
                    }
                }
                else if (Longbois[(j + thisHatRotInt) % 12].hasReverseHat)
                {
                    int calc = ((Longbois[(j + thisHatRotInt) % 12].hatRotInt + 12 - thisHatRotInt) % 6);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsLongboisHatReverseHat[(j + 12 - thisHatRotInt) % 12])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count > 0)
                    {
                        LongboisBool[j] = false;
                    }
                }
            }
        }
        else if (this.CompareTag("Reverse Hat"))
        {
            for (int i = 0; i < Neighbours.Length; i++)
            {
                if (Neighbours[(i + thisHatRotInt) % 6].hasHat)
                {
                    int calc = ((Neighbours[(i + thisHatRotInt) % 6].hatRotInt + 6 - thisHatRotInt) % 6);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsReverseHatHat[i])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count == 0)
                    {
                        NeighboursBool[i] = false;
                    }
                }
                else if (Neighbours[(i + thisHatRotInt) % 6].hasReverseHat)
                {
                    int calc = ((Neighbours[(i + thisHatRotInt) % 6].hatRotInt + 6 - thisHatRotInt) % 6);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsReverseHatReverseHat[i])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count == 0)
                    {
                        NeighboursBool[i] = false;
                    }
                }
            }
            for (int j = 0; j < Longbois.Length; j++)
            {
                if (Longbois[(j + thisHatRotInt) % 12].hasHat)
                {
                    int calc = ((Longbois[(j + thisHatRotInt) % 12].hatRotInt + 12 - thisHatRotInt) % 6);
                    Debug.Log("calc = " + calc);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsLongboisReverseHatHat[(j + 12 - thisHatRotInt) % 12])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count > 0)
                    {
                        LongboisBool[j] = false;
                    }
                }
                else if (Longbois[(j + thisHatRotInt) % 12].hasReverseHat)
                {
                    int calc = ((Longbois[(j + thisHatRotInt) % 12].hatRotInt + 12 - thisHatRotInt) % 6);
                    Debug.Log("calc = " + calc);
                    List<int> cribsList = new List<int>();
                    foreach (int c in cribsLongboisReverseHatReverseHat[(j + 12 - thisHatRotInt) % 12])
                    {
                        if (c == calc)
                        {
                            cribsList.Add(1);
                        }
                    }
                    if (cribsList.Count > 0)
                    {
                        LongboisBool[j] = false;
                    }
                }
            }
        }
        if (AllNeighboursValid())
        {
            isValid = true;
        }
        else
        {
            isValid = false;
        }
    }

    public bool AllNeighboursValid()
    {
        for (int i = 0; i < NeighboursBool.Length; i++)
            if (!NeighboursBool[i])
            {
                Debug.Log(((HexDirection)((i + thisHatRotInt) % 6)).Opposite());
                return false;
            }
        for (int j = 0; j < LongboisBool.Length; j++)
            if (!LongboisBool[j])
            {
                Debug.Log(((LongboiDirection)((j + thisHatRotInt) % 12)));
                return false;
            }
        return true;
    }
}
