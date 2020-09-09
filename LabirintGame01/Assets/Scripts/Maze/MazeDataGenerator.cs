using System.Collections.Generic;
using UnityEngine;

public class MazeDataGenerator
{
    #region definitions
    //0 - empty space
    //1 - wall
    //2 - chest
    //3 - player
    #endregion
    public float placementThreshold;    // chance of empty space
    private int[,] maze { get; set; }
    private List<(int, int)> emptyCells;
    public MazeDataGenerator()
    {
        placementThreshold = .1f;
        emptyCells = new List<(int, int)>();
    }
    private int GenerateIndex(List<int> generated, int minIndex, int maxIndex)
    {
        int index = Random.Range(minIndex, maxIndex);
        while (generated.Contains(index))
        {
            index = Random.Range(0, maxIndex);
        }
        return index;
    }
    private void GenerateChestsPos()
    {
        List<int> generated = new List<int>();
        int index;
        (int, int) cell;
        for (int i = 0; i<7; i++)
        {
            index = GenerateIndex(generated, 1, emptyCells.Count);
            cell = emptyCells[index];
            maze[cell.Item1, cell.Item2] = 2;
            generated.Add(index);
        }
    }
    private void GeneratePlayerPos()
    {
        for (int row = 0; row < maze.GetUpperBound(0); row++)
        {
            for (int col = 0; col < maze.GetUpperBound(1); col++)
            {
                if (maze[row, col] == 0)
                {
                    maze[row, col] = 3;
                    return;
                }
            }
        }
    }
    public int[,] FromDimensions(int sizeRows, int sizeCols)    // 2
    {
        maze = new int[sizeRows, sizeCols];
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {       
                if (i == 0 || j == 0 || i == rMax || j == cMax)
                {
                    maze[i, j] = 0;
                }
                else if (i % 2 == 0 && j % 2 == 0)
                {
                    if (Random.value > placementThreshold)
                    {
                        //3
                        maze[i, j] = 1;

                        int a = Random.value < .5 ? 0 : 1;
                        int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);
                        maze[i + a, j + b] = 1;
                    }
                }
                if (maze[i, j] == 0)
                {
                    emptyCells.Add((i, j));
                }
            }
        }
        GenerateChestsPos();
        GeneratePlayerPos();
        return maze;
    }
}