using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 worldPosition { get; set; }
    public Point cell { get; set; }
    public int PathLengthFromStart { get; set; }
    // Точка, из которой пришли в эту точку.
    public Node cameFrom { get; set; }
    // Примерное расстояние до цели (H).
    public int HeuristicEstimatePathLength { get; set; }
    // Ожидаемое полное расстояние до цели (F).
    public int EstimateFullPathLength
    {
        get
        {
            return PathLengthFromStart + HeuristicEstimatePathLength;
        }
    }
    
}
