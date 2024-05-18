using TMPro;
using UnityEngine;

public class Line : MonoBehaviour
{
    public int weightRange;
    [SerializeField] private TextMeshProUGUI weight;

    private void Start()
    {
        weightRange = Random.Range(1, 7);
        weight.text = weightRange.ToString();
    }
}
