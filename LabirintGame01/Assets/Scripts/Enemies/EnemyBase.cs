using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    [SerializeField] float speed;
    GameObject nodesContainer;

    public IMove move { get; set; }

    // Start is called before the first frame update
    public virtual void Start()
    {
        move = new DoNothing();
        ChestBase.InvokeEnemy += GenerateWay;
        nodesContainer = GameObject.Find("NodesContainer");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        move.Move();
    }
    private void GenerateWay(Vector3 targetPos)
    {
        Point targetCell = new Point(StaticConvertFunc.ReturnObjectCell(targetPos).Item1, StaticConvertFunc.ReturnObjectCell(targetPos).Item2);
        Point startCell = new Point(StaticConvertFunc.ReturnObjectCell(transform.position).Item1, StaticConvertFunc.ReturnObjectCell(transform.position).Item2);
        NodesGenerator generator = new NodesGenerator(startCell, targetCell);
        Debug.Log(generator);
        var nodes = generator.Generate();
        move = new FindWayMovement(transform, nodes, speed, this);
    }
    private void DestroyOldNodes()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("HelpNode");
        foreach (GameObject go in objects)
        {
            Destroy(go);
        }
    }
}
