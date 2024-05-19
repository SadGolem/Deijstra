using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private RandomGraphGenerator graphGenerator;
    [SerializeField] private Dijkstra dijkstra;


    public void GenerateGraph()
    {
        graphGenerator.Generate();
        Invoke("Initial", 0.01f);
    }

    private void Initial()
    {
        dijkstra.InitialGraph();
    }

    public void FindWay()
    {
        dijkstra.FindPath();
    }
}
