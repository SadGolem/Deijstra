using System.Linq;
using TMPro;
using Unity.VisualScripting;
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

    private void Clear()
    {
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");
        GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");
        foreach(GameObject sphere in spheres)
            Destroy(sphere);
        foreach (GameObject line in lines)
            Destroy(line);
    }

    [System.Obsolete]
    public void Generate()
    {
        Clear();
        for (int i = 0; i < numNodes; i++)
        {
            // Создать случайную позицию для сферы
            Vector3 position = Random.insideUnitSphere * 5;

            // Создать сферу
            var sphere = Instantiate(spherePrefab, position, Quaternion.identity);
            var text = sphere.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            // Заполнить текст буквой по алфавиту
            text.text = ((char)('A' + i)).ToString();

        }
        Invoke("GenerateGraphs", 0.01f);


    }

    private void GenerateGraphs()
    {
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
                if (Vector3.Distance(spheres[i].transform.position, spheres[j].transform.position) < 2 * 10 && Random.value < 0.8)
                {
                    GameObject line = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
                    line.transform.position = (spheres[i].transform.position + spheres[j].transform.position) / 2;
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
