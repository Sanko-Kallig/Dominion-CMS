using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public float WorkerCap;
    public GameObject WoodCutter;
    public GameObject Harvester;
    public GameObject StoneCutter;
    VillageController villageController;
    public enum BuildingType { Wood, Food, Stone, Housing};
    public BuildingType buildingType;
    public bool isPlaced;
    public float WorkerCount;
    // Start is called before the first frame update
    void Start()
    {
        WorkerCap = 1;
        villageController = GameObject.Find("CampFire(Clone)").GetComponent<VillageController>();
        //If the building type is a house, it will increase the population cap by 2.
        if (buildingType == BuildingType.Housing)
        {
            villageController.PopulationCap += 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaced && WorkerCount < WorkerCap)
        {
            SpawnWorker();
        }

    }

    private void SpawnWorker()
    {

        if (villageController.IdleVillagers.Count > 0 && WorkerCount < WorkerCap && buildingType != BuildingType.Housing)
        {
            GameObject targetedVillager = villageController.IdleVillagers[0];
            GameObject worker = null;

            //Will spawn a new instance of the correct type of worked based on the building type.
            switch (buildingType)
            {
                case BuildingType.Wood:
                    worker = Instantiate(WoodCutter);
                    worker.transform.position = targetedVillager.transform.position;
                    break;
                case BuildingType.Food:
                    worker = Instantiate(Harvester);
                    worker.transform.position = targetedVillager.transform.position;
                    break;
                case BuildingType.Stone:
                    worker = Instantiate(StoneCutter);
                    worker.transform.position = targetedVillager.transform.position;
                    break;
            }

            //Removes and Re-add the villager to the appropriate lists.
            villageController.IdleVillagers.Remove(targetedVillager);
            villageController.Villagers.Remove(targetedVillager);
            villageController.Workers.Add(worker);
            villageController.Villagers.Add(worker);
            Destroy(targetedVillager);
            WorkerCount++;
        }
        
    }
}
