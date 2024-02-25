using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlacer : PipeItemGenerator
{
    public PipeItem[] itemPrefabs;
    public PipeItem[] itemPrefabs2;

    public bool reverseColour;

    public override void GenerateItems(Pipe pipe)
    {
        reverseColour = Random.value > .5;

        if (reverseColour)
        {
            GenerateItem(itemPrefabs2, pipe);
        }
        else
        {
            GenerateItem(itemPrefabs, pipe);
        }
            
    }

    void GenerateItem(PipeItem[] pipeItems, Pipe pipe)
    {
        float angleStep = Random.Range(pipe.CurveSegmentCount - 3, pipe.CurveSegmentCount) * pipe.CurveAngle / pipe.CurveSegmentCount;
        PipeItem item = Instantiate<PipeItem>(
               pipeItems[Random.Range(0, pipeItems.Length)]);
        float pipeRotation =
           (Random.Range(0, pipe.pipeSegmentCount) + 0.5f) *
           360f / pipe.pipeSegmentCount;
        item.Position(pipe, angleStep, pipeRotation);
        pipe.reverseColour = reverseColour;
    }
}   
