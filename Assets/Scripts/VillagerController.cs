
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
    public VillageResourceController resourceController;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        resourceController = GameObject.Find("Level").GetComponent<VillageResourceController>();

        //Based on Villagerjob on creation of the Gameobject, this will create the appropriate villager class.
        switch (CurrentVillagerJob)
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
            case Villager.Job.Villager:
                CurrentVillager = new Villager(100, 100, 100, 100, GameObject.Find("CampFire(Clone)").transform);
                //TODO: Destination is being set here because of a spawning bug, where villagers get spawned in a wrong set of coordinates
                agent.SetDestination(GameObject.Find("CampFire(Clone)").transform.position);
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
        //Updates Villager food
        CurrentVillager.Food -= Time.deltaTime * 0.5f;
        Food = CurrentVillager.Food;
        Cargo = CurrentVillager.Cargo;

        //Tells when the villager should go and navigate to the food collection point.
       if (CurrentVillager.Food <= 30 && !CurrentVillager.GettingFood)
        {
            CurrentVillager.GettingFood = true;
            agent.SetDestination(CurrentVillager.FindClosestFood().transform.position);
        }
       //Based on the villager's job, the villager will go to the appropriate jobsite and collect resources, and deposit resources based on how much cargo the villager holds
       if(CurrentVillagerJob != Villager.Job.Villager)
        {
            if (!CurrentVillager.GettingFood && !CurrentVillager.DepostingCargo && CurrentVillager.Cargo < CurrentVillager.CollectionCap)
            {
                agent.SetDestination(CurrentVillager.FindClosestResource().transform.position);
            }
            if (!CurrentVillager.GettingFood && !CurrentVillager.DepostingCargo && (int)CurrentVillager.Cargo == (int)CurrentVillager.CollectionCap)
            {
                CurrentVillager.DepostingCargo = true;
                agent.SetDestination(CurrentVillager.FindClosestDeliveryPoint().transform.position);

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Collecting resources from nearest Resources Objects
        CurrentVillager.DoJob(other, resourceController);
        //Depositing resources to nearest Collection Objects
        CurrentVillager.Deposit(other, resourceController);
        //Retrieving food from nearest Food Storage;
        CurrentVillager.ConsumeFood(other, resourceController);
    }
}
