using System.Collections.Generic;
using UnityEngine;

public static class Graph
{
    // ������� ������ � �� �������
    private static Dictionary<Vector3, List<Vector3>> vertices = new Dictionary<Vector3, List<Vector3>>();

    public static Dictionary<Dictionary<GameObject, GameObject>, int> lines = new Dictionary<Dictionary<GameObject, GameObject>, int>(); // ��� �������� ������, �� ������� � ����� �����

    // �������� �������
    public static void AddVertex(Vector3 position)
    {
        vertices[position] = new List<Vector3>();
    }

    public static void AddGraph(GameObject sphere1, GameObject sphere2, int weight)
    {
        var dic = new Dictionary<GameObject, GameObject>
        {
            { sphere1, sphere2 }
        };
        lines.Add(dic, weight);
    }

    public static void RemoveVertex() {
        vertices.Clear();
    }

    // �������� �����
    public static void AddEdge(Vector3 from, Vector3 to)
    {
        vertices[from].Add(to);
        vertices[to].Add(from);
    }
}

