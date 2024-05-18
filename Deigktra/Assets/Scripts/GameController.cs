using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private RandomGraphGenerator graphGenerator;
    [SerializeField] private Dijkstra dijkstra;


    public void GenerateGraph()
    {
        graphGenerator.Generate();
    }


    public void FindWay()
    {
        /*dijkstra.();*/
    }
}
