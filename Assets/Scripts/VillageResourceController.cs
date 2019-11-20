using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VillageResourceController : MonoBehaviour
{
    public float StoredFood;
    public float StoredWood;
    public Text ScoreFood;
    public Text ScoreWood;
    public Text ScoreStone;

    private void Start()
    {
        ScoreFood.text = "Food: " + StoredFood.ToString();
        ScoreWood.text = "Wood: " + StoredWood.ToString();
    }
    private void Update()
    {
        ScoreFood.text = "Food: " + StoredFood.ToString();
        ScoreWood.text = "Wood: " + StoredWood.ToString();
    }


}
