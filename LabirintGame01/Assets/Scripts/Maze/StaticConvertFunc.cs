using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticConvertFunc
{
    public static Vector3 mazeStart { get; set; }
    public static float delta { get; set; }

    public static (int, int) ReturnObjectCell(Vector3 vector)
    {
        float x = vector.x;
        float z = vector.z;
        int col = Mathf.RoundToInt((vector.x - mazeStart.x) / delta);
        int row = Mathf.RoundToInt((vector.z - mazeStart.z) / delta);
        return (row, col);
    }
    public static Vector3 ReturnPositionInMaze(int row, int col)
    {
        return new Vector3(mazeStart.x + delta * col, mazeStart.y, mazeStart.z + delta * row);
    }
}
