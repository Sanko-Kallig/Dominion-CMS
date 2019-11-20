using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageController : MonoBehaviour
{
    public float PopulationCap;
    public float Population;
    public float SpawningTimer;
    private float Spawn;
    public GameObject Villager;
    public List<GameObject> Villagers;
    public List<GameObject> Workers;
    public List<GameObject> IdleVillagers;
    public bool IsPlaced;
    // Start is called before the first frame update
    void Start()
    {
        PopulationCap = 2;
        Spawn = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlaced)
        {

            SpawnVillager();
        }
    }

    private void SpawnVillager()
    {
        if (SpawningTimer < Spawn && Population < PopulationCap)
        {
            SpawningTimer += Time.deltaTime * 1f;
        }
        if (SpawningTimer >= Spawn && Population < PopulationCap)
        {
            GameObject tempWorker = Instantiate(Villager);
            tempWorker.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            Villagers.Add(tempWorker);
            IdleVillagers.Add(tempWorker);
            SpawningTimer = 0;
            Population++;

        }
    }
}
