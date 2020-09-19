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
    private EnemyController enemy;
    public FindWayMovement(Transform _obj, Transform _targetPoint, List<Transform> _helpNodes, float _speed, EnemyController _enemy)
    {
        obj = _obj;
        targetPoint = _targetPoint;
        helpNodes = _helpNodes;
        currentPoint = 0;
        speed = _speed;
        enemy = _enemy;
    }

    public bool Move()
    {
        if (currentPoint == helpNodes.Count - 1)
        {
            enemy.Move = new DoNothing();
            return false;
        }

        if (obj.position == targetPoint.position)
        {
            currentPoint++;
            if (currentPoint >= helpNodes.Count)
                currentPoint = 0;

            targetPoint = helpNodes[currentPoint];
        }
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

