using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesGenerator
{
    List<Point> points;
    List<Transform> nodes;
    Point startCell;
    Point targetCell;
    PathGenerator pathGenerator;

    public NodesGenerator(Point _startCell, Point _targetCell)
    {
        startCell = _startCell;
        targetCell = _targetCell;
        points = new List<Point>();
        nodes = new List<Transform>();
        pathGenerator = new PathGenerator(MazeConstructor.data, startCell, targetCell);
    }
    public List<Transform> Generate()
    {
        FillPoints();
        Debug.Log(points);
        InstHelpNodes();
        return nodes;

    }
    
    private void InstHelpNodes()
    {
        for (int i = 0; i < points.Count; i++)
        {
            GameObject node = new GameObject();
            node.transform.position = StaticConvertFunc.ReturnPositionInMaze(points[i].x, points[i].y);
            nodes.Add(node.transform);
            node.name = "node" + nodes.IndexOf(node.transform);
            node.tag = "HelpNode";
        }
    }
    private void FillPoints()
    {
        points = pathGenerator.FindPath();
    }
}
