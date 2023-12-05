using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlacer : PipeItemGenerator
{

    public PipeItem[] itemPrefabs;

    public override void GenerateItems(Pipe pipe)
    {
        float angleStep = Random.Range(pipe.CurveSegmentCount - 3, pipe.CurveSegmentCount) * pipe.CurveAngle / pipe.CurveSegmentCount;
        
        
            PipeItem item = Instantiate<PipeItem>(
                itemPrefabs[Random.Range(0, itemPrefabs.Length)]);
            float pipeRotation =
                (Random.Range(0, pipe.pipeSegmentCount) + 0.5f) *
                360f / pipe.pipeSegmentCount;
            item.Position(pipe, angleStep, pipeRotation);
        
    }
}   
