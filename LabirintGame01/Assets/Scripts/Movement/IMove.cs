using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    bool Move();
}

public class FindWayMovement : IMove
{
    private Transform obj;
    private Transform targetPoint;
    private List<Transform> helpNodes;
    private int currentPoint;
    private float speed;
    private IEnemy enemy;
    public FindWayMovement(Transform _obj, List<Transform> _helpNodes, float _speed, IEnemy _enemy)
    {
        obj = _obj;
        targetPoint = _helpNodes[1];
        helpNodes = _helpNodes;
        currentPoint = 0;
        speed = _speed;
        enemy = _enemy;
    }

    public bool Move()
    {
        if (currentPoint == helpNodes.Count - 1)
        {
            enemy.move = new DoNothing();
            return false;
        }

        if (obj.position == targetPoint.position)
        {
            currentPoint++;
            if (currentPoint >= helpNodes.Count)
                currentPoint = 0;

            targetPoint = helpNodes[currentPoint];
        }
        obj.LookAt(targetPoint);
        obj.position = Vector3.MoveTowards(obj.position, targetPoint.position, speed * Time.deltaTime);
        return true;
    }
}
public class DoNothing : IMove
{
    public bool Move()
    {
        return true;
    }
}

