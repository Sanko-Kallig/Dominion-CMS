
using System;
using UnityEngine;
using UnityEngine.AI;

public class VillagerController : MonoBehaviour
{
    public Villager CurrentVillager;
    public NavMeshAgent agent;
    public Villager.Job CurrentVillagerJob;
    public float Food;
    public float Cargo;
    public ResourceController resourceController;
    // Start is called before the first frame update
    void Start()
    {
        switch(CurrentVillagerJob)
        {
            case Villager.Job.Harvester:
                CurrentVillager = new Harvester(100, 100, 100, 100, transform);
                break;
            case Villager.Job.Woodcutter:
                CurrentVillager = new WoodCutter(100, 100, 100, 100, transform);
                break;
            case Villager.Job.StoneCutter:
                CurrentVillager = new StoneCutter(100, 100, 100, 100, transform);
                break;
        }
        
        CurrentVillager.GettingFood = false;
        CurrentVillager.DepostingCargo = false;
        CurrentVillager.VillagerJob = CurrentVillagerJob;
        CurrentVillager.CollectionCap = 20;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentVillager.Food -= Time.deltaTime * 1;
        Food = CurrentVillager.Food;
        Cargo = CurrentVillager.Cargo;

       if (CurrentVillager.Food <= 30 && !CurrentVillager.GettingFood)
        {
            CurrentVillager.GettingFood = true;
            agent.SetDestination(CurrentVillager.FindClosestFood().transform.position);
        }
       if(!CurrentVillager.GettingFood && !CurrentVillager.DepostingCargo && CurrentVillager.Cargo < CurrentVillager.CollectionCap)
        {
                agent.SetDestination(CurrentVillager.FindClosestResource().transform.position);
        }
       if(!CurrentVillager.GettingFood && !CurrentVillager.DepostingCargo && (int)CurrentVillager.Cargo == (int)CurrentVillager.CollectionCap)
        {
                CurrentVillager.DepostingCargo = true;
                agent.SetDestination(CurrentVillager.FindClosestDeliveryPoint().transform.position);

        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (!CurrentVillager.GettingFood && CurrentVillager.GetType().Name == "Harvester" && other.tag == "Grain")
        {
            if (CurrentVillager.Cargo <= CurrentVillager.CollectionCap)
            {
                CurrentVillager.Cargo += Time.deltaTime * 2;

            }
        }
        if (!CurrentVillager.GettingFood && CurrentVillager.GetType().Name == "WoodCutter" && other.tag == "Tree")
        {
            if (CurrentVillager.Cargo <= CurrentVillager.CollectionCap)
            {
                CurrentVillager.Cargo += Time.deltaTime * 2;

            }
        }
        if (!CurrentVillager.GettingFood && CurrentVillager.GetType().Name == "WoodCutter" && other.tag == "WoodPile")
        {
            resourceController.StoredWood += (int)CurrentVillager.Cargo;
            CurrentVillager.Cargo = 0;
            CurrentVillager.DepostingCargo = false;
        }
        if (!CurrentVillager.GettingFood && CurrentVillager.GetType().Name == "Harvester" && other.tag == "Mill")
        {
            resourceController.StoredFood += (int)CurrentVillager.Cargo;
            CurrentVillager.Cargo = 0;
            CurrentVillager.DepostingCargo = false;

        }
        if (other.tag == "Mill" && CurrentVillager.GettingFood)
        {
            float temp;
            temp = CurrentVillager.MaxFood - CurrentVillager.Food;
            resourceController.StoredFood -= (int)temp;
            CurrentVillager.Food += temp;
            CurrentVillager.GettingFood = false;
        }
    }
    //private void o(Collision collision)
    //{

    //    if (!CurrentVillager.GettingFood && CurrentVillager.GetType().Name == "Harvester" && collision.collider.tag == "Grain")
    //    {
    //        if(CurrentVillager.Cargo <= CurrentVillager.CollectionCap)
    //        {
    //            CurrentVillager.Cargo += Time.deltaTime * 10;
                
    //        }
    //    }
    //    if (!CurrentVillager.GettingFood && CurrentVillager.GetType().Name == "Harvester" && collision.collider.tag == "Mill")
    //    {
    //        resourceController.StoredFood += (int)CurrentVillager.Cargo;
    //        CurrentVillager.Cargo = 0;
    //        CurrentVillager.DepostingCargo = false;

    //    }
    //    if (collision.collider.tag == "Mill" && CurrentVillager.GettingFood)
    //    {
    //        float temp;
    //        temp = CurrentVillager.MaxFood - CurrentVillager.Food;
    //        resourceController.StoredFood -= (int)temp;
    //        CurrentVillager.Food += temp;
    //        CurrentVillager.GettingFood = false;
    //    }
    //}
}
