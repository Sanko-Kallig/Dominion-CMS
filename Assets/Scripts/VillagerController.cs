
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
        CurrentVillager.Food -= Time.deltaTime * 0.5f;
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
        CurrentVillager.DoJob(other, resourceController);
        CurrentVillager.Deposit(other, resourceController);
        CurrentVillager.ConsumeFood(other, resourceController);
    }
}
