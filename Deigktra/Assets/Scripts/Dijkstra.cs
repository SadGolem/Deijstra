using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utils;

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

    // ������������� �����
    void Start()
    {
        graph = new Dictionary<GameObject, List<(GameObject, float)>>();
        foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("Sphere"))
        {
            graph[sphere] = new List<(GameObject, float)>();
        }

        // �������� ����� ����� �� ������ ���������� ����� �������
        foreach (GameObject sphere1 in graph.Keys)
        {
            foreach (GameObject sphere2 in graph.Keys)
            {
                if (sphere1 != sphere2)
                {
                    float distance = Vector3.Distance(sphere1.transform.position, sphere2.transform.position);
                    graph[sphere1].Add((sphere2, distance));
                    graph[sphere2].Add((sphere1, distance));
                }
            }
        }
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
                        FindPath();
                    }
                }
            }
        }
    }


    // ����� ����������� ���� � ������� ��������� ��������
    private void FindPath()
    {
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
 


