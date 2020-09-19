using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject nodesContainer;

    public IMove Move;

    // Start is called before the first frame update
    void Start()
    {
        Move = new DoNothing();
        ChestBase.InvokeEnemy += GenerateWay;
    }

    // Update is called once per frame
    void Update()
    {
        Move.Move();
    }
    private void GenerateWay(Vector3 targetPos)
    {
        List<Transform> helpNodes = new List<Transform>();
        (int, int) targetCell = StaticConvertFunc.ReturnObjectCell(targetPos);
        (int, int) startCell = StaticConvertFunc.ReturnObjectCell(transform.position);
        Debug.Log(startCell);
        List<(int, int)> points = PathGenerator.FindPath(MazeConstructor.data, startCell, targetCell);
        InstHelpNodes(helpNodes, points);
        Move = new FindWayMovement(transform, helpNodes[1], helpNodes, speed, this);
        Debug.Log("Path");
    }
    private void InstHelpNodes(List<Transform> helpNodes, List<(int, int)> points)
    {
        for (int i = 0; i < points.Count; i ++)
        {
            GameObject node = new GameObject();
            node.transform.position = StaticConvertFunc.ReturnPositionInMaze(points[i].Item1, points[i].Item2);
            helpNodes.Add(node.transform);
            node.name = "node" + helpNodes.IndexOf(node.transform);
            node.tag = "HelpNode";
        }
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
