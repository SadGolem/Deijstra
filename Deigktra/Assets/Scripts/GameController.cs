using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private RandomGraphGenerator graphGenerator;


    public void GenerateGraph()
    {
        graphGenerator.Generate();
    }


    public void FindWay()
    {

    }
}
