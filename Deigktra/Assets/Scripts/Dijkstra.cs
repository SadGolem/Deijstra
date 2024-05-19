using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    // ������ ����� � �� �������
    private Dictionary<GameObject, List<(GameObject, float)>> graph;

    // ��������� � �������� �����
    private GameObject startSphere;
    private GameObject endSphere;

    // ������� ���������� �� ��������� ����� �� ���� ��������� �����
    private Dictionary<GameObject, float> distances;

    // ������� ���������������� ��� �������������� ����������� ����
    private Dictionary<GameObject, GameObject> predecessors;

    private void Start()
    {
        Invoke("InitialGraph", 0.02f);
    }

    // ��������� ������ ���� �� �����
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject sphere = hit.collider.gameObject;
                if (sphere.tag == "Sphere")
                {
                    // ���������� ��������� ��� �������� ����� � ����������� �� ����, ������� �� �����
                    if (startSphere == null)
                    {
                        startSphere = sphere;
                        var s = sphere.GetComponentInChildren<TextMeshProUGUI>();
                        Debug.Log("������� ��������� �����: " + s.text);
                    }
                    else if (endSphere == null)
                    {
                        endSphere = sphere;
                        var s = sphere.GetComponentInChildren<TextMeshProUGUI>();
                        Debug.Log("������� �������� �����: " + s.text);
                    }
                }
            }
        }
    }

    //�������������
    public void InitialGraph()
    {
        graph = new Dictionary<GameObject, List<(GameObject, float)>>();
        foreach (var line in Graph.lines)
        {
            var sphere1 = line.Key.Keys.First();
            var sphere2 = line.Key.Values.First();
            var distance = line.Value;

            if (!graph.ContainsKey(sphere1))
            {
                graph[sphere1] = new List<(GameObject, float)>();
            }
            if (!graph.ContainsKey(sphere2))
            {
                graph[sphere2] = new List<(GameObject, float)>();
            }

            graph[sphere1].Add((sphere2, distance));
            graph[sphere2].Add((sphere1, distance));
        }
    }

    // ����� ����������� ���� � ������� ��������� ��������
    public void FindPath()
    {
        if (endSphere == null) return;
        // ������������� ��������
        distances = new Dictionary<GameObject, float>();
        predecessors = new Dictionary<GameObject, GameObject>();

        foreach (GameObject sphere in graph.Keys)
        {
            distances[sphere] = float.PositiveInfinity; // ��������� ���������� ���������� �� ���� ����� � �������������
            predecessors[sphere] = null; // ��������� ���������������� ��� ���� ����� �� null
        }

        // ���������� �� ��������� ����� ����� 0
        distances[startSphere] = 0;

        // ������ �������������� �����
        List<GameObject> unvisited = new List<GameObject>(graph.Keys);

        // ���� ���� �������������� �����
        while (unvisited.Count > 0)
        {
            // ��������� �������������� ����� � ���������� �����������
            GameObject current = unvisited[0];
            for (int i = 1; i < unvisited.Count; i++)
            {
                if (distances[unvisited[i]] < distances[current])
                {
                    current = unvisited[i];
                }
            }

            // ���� ��� �������� �����, �� ����� ���������� ����
            if (current == endSphere)
            {
                break;
            }

            // ������������ ������� ������� �����
            foreach ((GameObject neighbor, float weight) in graph[current])
            {
                // ���������� ����� ���������� �� ������
                float newDistance = distances[current] + weight;

                // ���� ����� ���������� ������, ��� ����������
                if (newDistance < distances[neighbor])
                {
                    // ���������� ���������� � ���������������
                    distances[neighbor] = newDistance;
                    predecessors[neighbor] = current;
                }
            }

            // �������� ������� ����� �� ������ ��������������
            unvisited.Remove(current);
        }

        // ����� ����������� ����
        if (predecessors[endSphere] != null)
        {
            // �������������� ���� �� �������� ����� � ���������
            List<GameObject> path = new List<GameObject>();
            GameObject current = endSphere;
            while (current != null)
            {
                path.Add(current);
                current = predecessors[current];
            }

            // ����������� ����
            path.Reverse();
            string pathString = "���������� ����: ";
            foreach (GameObject sphere in path)
            {
                pathString += sphere.GetComponentInChildren<TextMeshProUGUI>().text + " -> ";
            }
            pathString = pathString.Substring(0, pathString.Length - 3);
            Debug.Log(pathString);
        }
        else
        {
            // ���� ���� �� ����������
            Debug.Log("���� ����� ���������� ������� �� ����������");
        }
    }
}
 


