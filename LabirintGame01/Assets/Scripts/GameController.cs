using System;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]               // 1

public class GameController : MonoBehaviour
{
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
    void Start()
    {
        instance.generator = GetComponent<MazeConstructor>();
        instance.generator.GenerateNewMaze(17, 29);
    }
}
