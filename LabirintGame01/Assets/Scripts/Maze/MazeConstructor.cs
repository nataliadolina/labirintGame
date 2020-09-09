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
    private float mazeDelta;
    [SerializeField] private Transform wallsContainer;
    [SerializeField] private Transform chestsContainer;
    [SerializeField] private Transform mazeStart;
    private MazeDataGenerator dataGenerator;

    private List<(int, int)> emptyCells;
    private (int, int) playerPosition;

    //2
    public int[,] data
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
                pos = ReturnPositionInMaze(row, col);
                obj = null;
                if (data[row, col] == 1)
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
                else if (data[row, col] == 2)
                {
                    obj = Instantiate(chest, pos, Quaternion.identity, chestsContainer);
                }
                else if (data[row, col] == 3)
                {
                    pos.y = 0;
                    obj = Instantiate(player, pos, Quaternion.identity);
                }
            }
        }
    }
    private void BuildLevel()
    {
        DestroyOldMaze();
        InstantiatePlayer();
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
    private Vector3 ReturnPositionInMaze(int row, int col)
    {
        return new Vector3(mazeStart.position.x + mazeDelta * col, mazeStart.position.y, mazeStart.position.z + mazeDelta * row);
    }
    private void InstantiatePlayer()
    {
        Vector3 pos;
        for (int row = 0; row < data.GetUpperBound(0); row++)
        {
            for (int col = 0; col < data.GetUpperBound(1); col++)
            {
                if (data[row, col] == 0)
                {
                    pos = ReturnPositionInMaze(row, col);
                    Instantiate(player, pos, Quaternion.identity);
                    return;
                }
            }
        }
    }
    
    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }

        data = dataGenerator.FromDimensions(sizeRows, sizeCols);
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
