using TMPro;
using UnityEngine;

public class RandomGraphGenerator : MonoBehaviour
{
    // ���������� ����� � �����
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
        foreach (GameObject sphere in spheres)
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
            // ������� ��������� ������� ��� �����
            Vector3 position = Random.insideUnitSphere * 5;

            // ������� �����
            var sphere = Instantiate(spherePrefab, position, Quaternion.identity);
            var text = sphere.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            // ��������� ����� ������ �� ��������
            text.text = ((char)('A' + i)).ToString();

        }
        Invoke("GenerateGraphs", 0.01f);


    }

    private void GenerateGraphs()
    {
        Graph.RemoveLines();
        // �������� ��� ����� � �����
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");

        // �������� ����� � ����
        foreach (GameObject sphere in spheres)
        {
            Graph.AddVertex(sphere.transform.position);
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
                    Graph.AddEdge(spheres[i].transform.position, spheres[j].transform.position);
                    var lineWeight = line.GetComponent<Line>().weightRange;
                    var lineWeightText = line.GetComponent<Line>().weight;
                    lineWeight = Random.Range(1, 7);
                    lineWeightText.text = lineWeight.ToString();
                    Graph.AddGraph(spheres[i], spheres[j], lineWeight);
                }
            }
        }
    }
}
