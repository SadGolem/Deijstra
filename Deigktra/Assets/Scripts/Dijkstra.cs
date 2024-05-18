using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dijkstra : MonoBehaviour
{
    /*// ������ ����� � �� �������
    private Dictionary<string, List<(string, float)>> graph;

    // ��������� � �������� �����
    public Text startText;
    public Text endText;

    public void FindPath()
    {
        // ��������� ��������� � �������� �����
        string start = startText.text;
        string end = endText.text;

        // �������� �����
        if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
        {
            Debug.LogError("������� ��������� � �������� �����");
            return;
        }

        // �������������
        var distances = new Dictionary<string, float>();
        var previous = new Dictionary<string, string>();
        foreach (var node in graph.Keys)
        {
            distances[node] = float.PositiveInfinity;
            previous[node] = null;
        }
        distances[start] = 0;

        // �������� ��������
        var pq = new Queue<(string, float)>();
        pq.Enqueue((start, 0));
        while (pq.Count > 0)
        {
            // ��������� ���� � ����������� �����������
            var current = pq.Dequeue();

            // ���� �������� �������� �����, ������� ����
            if (current.Item1 == end)
            {
                PrintPath(start, end, previous);
                return;
            }

            // ������������ �������
            foreach (var neighbor in graph[current.Item1])
            {
                // ������� ����������
                var distance = current.Item2 + neighbor.Item2;

                // ���������� ���������� � ����������� ����
                if (distance < distances[neighbor.Item1])
                {
                    distances[neighbor.Item1] = distance;
                    previous[neighbor.Item1] = current.Item1;
                    pq.Enqueue((neighbor.Item1, distance));
                }
            }
        }

        // ���� ���� �� ������, ������� ���������
        Debug.LogError("���� �� ������");
    }

    // ����� ����
    private void PrintPath(string start, string end, Dictionary<string, string> previous)
    {
        var path = new List<string>();
        var current = end;
        while (current != null)
        {
            path.Add(current);
            current = previous[current];
        }
        path.Reverse();
        Debug.Log($"����: {string.Join(" -> ", path)}");
    }*/
}

