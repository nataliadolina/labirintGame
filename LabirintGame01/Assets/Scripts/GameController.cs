using System;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]               // 1

public class GameController : MonoBehaviour
{
    public int rows;
    public int cols;
    public static GameController instance;
    public MazeConstructor generator;
    private void Awake()
    {
        
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
    }
    private void OnDestroy()
    {
        instance = null;
    }
    private void Start()
    {
        instance.generator = GetComponent<MazeConstructor>();
        instance.generator.GenerateNewMaze(rows, cols);
    }
}
