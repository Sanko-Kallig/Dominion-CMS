using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public float StoredFood;
    public Text score;

    private void Start()
    {
        score.text = StoredFood.ToString();
    }
    private void Update()
    {
        score.text = StoredFood.ToString();
    }


}
