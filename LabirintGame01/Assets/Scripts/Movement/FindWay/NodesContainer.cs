using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesContainer
{
    private List<Node> nodes = new List<Node>();
    public NodesContainer()
    {
        
    }
    public void AddNode((int, int) cell, Vector3 position)
    {
        nodes.Add(new Node());
    }
}
