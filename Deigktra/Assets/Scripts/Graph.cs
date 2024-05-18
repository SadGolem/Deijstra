using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    // Словарь вершин и их позиций
    private Dictionary<Vector3, List<Vector3>> vertices = new Dictionary<Vector3, List<Vector3>>();

    // Добавить вершину
    public void AddVertex(Vector3 position)
    {
        vertices[position] = new List<Vector3>();
    }

    // Добавить ребро
    public void AddEdge(Vector3 from, Vector3 to)
    {
        vertices[from].Add(to);
        vertices[to].Add(from);
    }
}

