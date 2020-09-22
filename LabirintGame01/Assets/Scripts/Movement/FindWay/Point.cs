using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public int x { get; private set; }
    public int y { get; private set; }
    public Point(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
    public Vector3 ReturnPosInMaze()
    {
        return StaticConvertFunc.ReturnPositionInMaze(x, y);
    }
}
