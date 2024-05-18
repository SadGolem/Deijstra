using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dijkstra : MonoBehaviour
{
    /*// Список узлов и их соседей
    private Dictionary<string, List<(string, float)>> graph;

    // Начальная и конечная точки
    public Text startText;
    public Text endText;

    public void FindPath()
    {
        // Получение начальной и конечной точек
        string start = startText.text;
        string end = endText.text;

        // Проверка ввода
        if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
        {
            Debug.LogError("Введите начальную и конечную точки");
            return;
        }

        // Инициализация
        var distances = new Dictionary<string, float>();
        var previous = new Dictionary<string, string>();
        foreach (var node in graph.Keys)
        {
            distances[node] = float.PositiveInfinity;
            previous[node] = null;
        }
        distances[start] = 0;

        // Алгоритм Дейкстры
        var pq = new Queue<(string, float)>();
        pq.Enqueue((start, 0));
        while (pq.Count > 0)
        {
            // Получение узла с минимальным расстоянием
            var current = pq.Dequeue();

            // Если достигли конечной точки, выводим путь
            if (current.Item1 == end)
            {
                PrintPath(start, end, previous);
                return;
            }

            // Рассмотрение соседей
            foreach (var neighbor in graph[current.Item1])
            {
                // Рассчет расстояния
                var distance = current.Item2 + neighbor.Item2;

                // Обновление расстояния и предыдущего узла
                if (distance < distances[neighbor.Item1])
                {
                    distances[neighbor.Item1] = distance;
                    previous[neighbor.Item1] = current.Item1;
                    pq.Enqueue((neighbor.Item1, distance));
                }
            }
        }

        // Если путь не найден, выводим сообщение
        Debug.LogError("Путь не найден");
    }

    // Вывод пути
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
        Debug.Log($"Путь: {string.Join(" -> ", path)}");
    }*/
}

