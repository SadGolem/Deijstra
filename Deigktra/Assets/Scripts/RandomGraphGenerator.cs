using System.Linq;
using UnityEngine;

public class RandomGraphGenerator : MonoBehaviour
{
    // Количество узлов в графе
    public int numNodes;

    public GameObject linePrefab;
    public GameObject spherePrefab;
    public float lineWidth = 0.1f;

    void Start()
    {
        Generate();
    }

    [System.Obsolete]
    public void Generate()
    {
        for (int i = 0; i < numNodes; i++)
        {
            // Создать случайную позицию для сферы
            Vector3 position = Random.insideUnitSphere * 5;

            // Создать сферу
            Instantiate(spherePrefab, position, Quaternion.identity);
        }

        // Получить все сферы в сцене
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");

        // Создать граф
        Graph graph = new Graph();

        // Добавить сферы в граф
        foreach (GameObject sphere in spheres)
        {
            graph.AddVertex(sphere.transform.position);
        }

        for (int i = 0; i < numNodes; i++)
        {
            for (int j = i + 1; j < numNodes; j++)
            {
                // Check if the spheres are not too far apart
                if (Vector3.Distance(spheres[i].transform.position, spheres[j].transform.position) < 2 * 10)
                {
                    // Create a line connecting the two spheres
                    GameObject line = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
                    LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
                    lineRenderer.SetWidth(lineWidth, lineWidth);
                    lineRenderer.SetPosition(0, spheres[i].transform.position);
                    lineRenderer.SetPosition(1, spheres[j].transform.position);
                    graph.AddEdge(spheres[i].transform.position, spheres[j].transform.position);
                }
            }

        }
    }

}
