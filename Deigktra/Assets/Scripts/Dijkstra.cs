using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    // Список узлов и их соседей
    private Dictionary<GameObject, List<(GameObject, float)>> graph;

    // Начальная и конечная точки
    private GameObject startSphere;
    private GameObject endSphere;

    // Словарь расстояний от начальной точки до всех остальных точек
    private Dictionary<GameObject, float> distances;

    // Словарь предшественников для восстановления кратчайшего пути
    private Dictionary<GameObject, GameObject> predecessors;

    private void Start()
    {
        Invoke("InitialGraph", 0.02f);
    }

    // Обработка щелчка мыши по сфере
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
                    // Назначение начальной или конечной точки в зависимости от того, выбрана ли сфера
                    if (startSphere == null)
                    {
                        startSphere = sphere;
                        var s = sphere.GetComponentInChildren<TextMeshProUGUI>();
                        Debug.Log("Выбрана начальная точка: " + s.text);
                    }
                    else if (endSphere == null)
                    {
                        endSphere = sphere;
                        var s = sphere.GetComponentInChildren<TextMeshProUGUI>();
                        Debug.Log("Выбрана конечная точка: " + s.text);
                    }
                }
            }
        }
    }

    //Инициализация
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

    // Поиск кратчайшего пути с помощью алгоритма Дейкстры
    public void FindPath()
    {
        if (endSphere == null) return;
        // Инициализация словарей
        distances = new Dictionary<GameObject, float>();
        predecessors = new Dictionary<GameObject, GameObject>();

        foreach (GameObject sphere in graph.Keys)
        {
            distances[sphere] = float.PositiveInfinity; // Установка начального расстояния до всех точек в бесконечность
            predecessors[sphere] = null; // Установка предшественников для всех точек на null
        }

        // Расстояние до начальной точки равно 0
        distances[startSphere] = 0;

        // Список необработанных точек
        List<GameObject> unvisited = new List<GameObject>(graph.Keys);

        // Пока есть необработанные точки
        while (unvisited.Count > 0)
        {
            // Получение необработанной точки с наименьшим расстоянием
            GameObject current = unvisited[0];
            for (int i = 1; i < unvisited.Count; i++)
            {
                if (distances[unvisited[i]] < distances[current])
                {
                    current = unvisited[i];
                }
            }

            // Если это конечная точка, мы нашли кратчайший путь
            if (current == endSphere)
            {
                break;
            }

            // Рассмотрение соседей текущей точки
            foreach ((GameObject neighbor, float weight) in graph[current])
            {
                // Рассчитаем новое расстояние до соседа
                float newDistance = distances[current] + weight;

                // Если новое расстояние меньше, чем предыдущее
                if (newDistance < distances[neighbor])
                {
                    // Обновление расстояния и предшественника
                    distances[neighbor] = newDistance;
                    predecessors[neighbor] = current;
                }
            }

            // Удаление текущей точки из списка необработанных
            unvisited.Remove(current);
        }

        // Вывод кратчайшего пути
        if (predecessors[endSphere] != null)
        {
            // Восстановление пути от конечной точки к начальной
            List<GameObject> path = new List<GameObject>();
            GameObject current = endSphere;
            while (current != null)
            {
                path.Add(current);
                current = predecessors[current];
            }

            // Отображение пути
            path.Reverse();
            string pathString = "Кратчайший путь: ";
            foreach (GameObject sphere in path)
            {
                pathString += sphere.GetComponentInChildren<TextMeshProUGUI>().text + " -> ";
            }
            pathString = pathString.Substring(0, pathString.Length - 3);
            Debug.Log(pathString);
        }
        else
        {
            // Если пути не существует
            Debug.Log("Пути между выбранными точками не существует");
        }
    }
}
 


