using UnityEngine;
using System.Collections.Generic;

public class MazeConstructor : MonoBehaviour
{
    //1
    public bool showDebug;
    private Vector3 lastPoint;
    [SerializeField] private GameObject mazeWall;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject mazeWallWithTorch;
    public static float mazeDelta;
    [SerializeField] private Transform wallsContainer;
    [SerializeField] private Transform chestsContainer;

    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform enemiesContainer;
    public Transform mazeStart;
    private MazeDataGenerator dataGenerator;

    public NodesContainer nodes;

    private List<(int, int)> emptyCells;
    private (int, int) playerPosition;

    //2
    public static int[,] data
    {
        get; private set;
    }

    //3
    void Awake()
    {
        mazeDelta = mazeWall.transform.localScale.x;
        // default to walls surrounding a single empty cell
        data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
        dataGenerator = new MazeDataGenerator();
        StaticConvertFunc.delta = mazeDelta;
        StaticConvertFunc.mazeStart = mazeStart.position;

    }
    
    private void BuildMaze()
    {
        bool withTorch = false;
        Vector3 pos;
        GameObject obj;
        for (int row=0; row < data.GetUpperBound(0); row++)
        {
            for (int col=0; col < data.GetUpperBound(1); col++)
            {
                pos = StaticConvertFunc.ReturnPositionInMaze(row, col);
                obj = null;
                if (data[row, col] == dataGenerator.wall)
                {
                    
                    if (!withTorch)
                    {
                        obj = Instantiate(mazeWall, pos, Quaternion.identity, wallsContainer);
                        withTorch = true;
                    }
                        
                    else
                    {
                        obj = Instantiate(mazeWallWithTorch, pos, Quaternion.identity, wallsContainer);
                        withTorch = false;
                    } 
                }
                else if (data[row, col] == dataGenerator.chest)
                {
                    pos.y = 2f;
                    obj = Instantiate(chest, pos, Quaternion.identity, chestsContainer);
                }
                else if (data[row, col] == dataGenerator.player)
                {
                    pos.y = 0;
                    obj = Instantiate(player, pos, Quaternion.identity);
                }
                else if (data[row, col] == dataGenerator.enemy)
                {
                    pos.y = 1f;
                    obj = Instantiate(enemy, pos, Quaternion.identity, enemiesContainer);
                }
            }
        }
    }
    
    private void BuildLevel()
    {
        DestroyOldMaze();
        BuildMaze();
    }
    private void DestroyOldMaze()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Generated");
        foreach (GameObject go in objects)
        {
            Destroy(go);
        }
    }
    
    
    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }
        data = dataGenerator.FromDimensions(sizeRows, sizeCols);
        Debug.Log("Generated");
        BuildMaze();
    }
    void OnGUI()
    {
        //1
        if (!showDebug)
        {
            return;
        }

        //2
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";

        //3
        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    msg += "....";
                }
                else
                {
                    msg += "==";
                }
            }
            msg += "\n";
        }

        //4
        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }
}
