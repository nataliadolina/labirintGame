﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    #region описание алгоритма нахождения пути
    //1. Создается 2 списка вершин — ожидающие рассмотрения и уже рассмотренные.
    //В ожидающие добавляется точка старта, список рассмотренных пока пуст.
    //2. Для каждой точки рассчитывается F = G + H. G — расстояние от старта до точки,
    //H — примерное расстояние от точки до цели. О расчете этой величины я расскажу позднее.
    //Так же каждая точка хранит ссылку на точку, из которой в нее пришли.
    //3. Из списка точек на рассмотрение выбирается точка с наименьшим F. Обозначим ее X.
    //4. Если X — цель, то мы нашли маршрут.
    //5. Переносим X из списка ожидающих рассмотрения в список уже рассмотренных.
    //6. Для каждой из точек, соседних для X (обозначим эту соседнюю точку Y), делаем следующее:
    //7. Если Y уже находится в рассмотренных — пропускаем ее.
    //8. Если Y еще нет в списке на ожидание — добавляем ее туда,
    //запомнив ссылку на X и рассчитав Y.G (это X.G + расстояние от X до Y) и Y.H.
    //9. Если же Y в списке на рассмотрение — проверяем, если X.G + расстояние от X до Y < Y.G,
    //значит мы пришли в точку Y более коротким путем, заменяем Y.G на X.G + расстояние от X до Y,
    //а точку, из которой пришли в Y на X.
    //10. Если список точек на рассмотрение пуст, 
    //а до цели мы так и не дошли — значит маршрут не существует.
    #endregion
    private Transform tr;
    private int[,] field;
    Point start;
    Point goal;
    public PathGenerator(int[,] _field, Point _start, Point _goal)
    {
        field = _field;
        start = _start;
        goal = _goal;
    }
    public List<Point> FindPath()
    {
        // Шаг 1.
        var closedSet = new List<Node>();
        var openSet = new List<Node>();
        // Шаг 2.
        Node startNode = new Node()
        {
            cell = start,
            HeuristicEstimatePathLength = GetHeuristicPathLength(start, goal),
            PathLengthFromStart = 0,
            cameFrom = null,
        };
        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            Debug.Log(openSet.Count);
            // Шаг 3.
            Node currentNode = openSet.OrderBy(node => node.EstimateFullPathLength).First();
            // Шаг 4.
            if (isEquel(currentNode.cell, goal))
                return GetPathForNode(currentNode);
            // Шаг 5.
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            // Шаг 6.
            foreach (var neighbourNode in GetNeighbours(currentNode, goal, field))
            {
                // Шаг 7.
                if (closedSet.Count(node => isEquel(node.cell, neighbourNode.cell)) > 0)
                    continue;
                var openNode = openSet.FirstOrDefault(node =>
                  isEquel(node.cell, neighbourNode.cell));
                // Шаг 8.
                if (openNode == null)
                    openSet.Add(neighbourNode);
                else
                {
                    if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        // Шаг 9.
                        openNode.cameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }
        }
        // Шаг 10.
        return null;
    }
    private bool isEquel(Point cell1, Point cell2)
    {
        return cell1.x == cell2.x && cell1.y == cell2.y;
    }
    private int GetDistanceBetweenNeighbours()
    {
        return 1;
    }
    private int GetHeuristicPathLength(Point from, Point to)
    {
        return Mathf.Abs(from.x - to.x) + Mathf.Abs(from.y - to.y);
    }
    private List<Point> GetPathForNode(Node pathNode)
    {
        var result = new List<Point>();
        var currentNode = pathNode;
        while (currentNode != null)
        {
            result.Add(currentNode.cell);
            currentNode = currentNode.cameFrom;
        }
        result.Reverse();
        return result;
    }
    private List<Node> GetNeighbours(Node pathNode, Point goal, int[,] field)
    {
        var result = new List<Node>(); 

        // Соседними точками являются соседние по стороне клетки.
        Point[] neighbourPoints = new Point[4];
        neighbourPoints[0] = new Point(pathNode.cell.x + 1, pathNode.cell.y);
        neighbourPoints[1] = new Point(pathNode.cell.x - 1, pathNode.cell.y);
        neighbourPoints[2] = new Point(pathNode.cell.x, pathNode.cell.y + 1);
        neighbourPoints[3] = new Point(pathNode.cell.x, pathNode.cell.y - 1);

        foreach (var point in neighbourPoints)
        {
            // Проверяем, что не вышли за границы карты.
            if (point.x < 0 || point.x >= field.GetLength(0))
                continue;
            if (point.y < 0 || point.y >= field.GetLength(1))
                continue;
            // Проверяем, что по клетке можно ходить.
            if ((field[point.x, point.y] == -1))
                continue;
            // Заполняем данные для точки маршрута.
            var neighbourNode = new Node()
            {
                cell = point,
                cameFrom = pathNode,
                PathLengthFromStart = pathNode.PathLengthFromStart +
                GetDistanceBetweenNeighbours(),
                HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
            };
            result.Add(neighbourNode);
        }
        return result;
    }
}
