using UnityEngine;
using System.Collections;

public class Harvester : Villager
{
    public float CollectedFood;
    public Harvester(float hitpoints, float food, float comfort, float maxFood, Transform transform) : base(hitpoints, food, comfort, maxFood, transform)
    {
    }
    public override GameObject FindClosestResource()
    {
        return FindClosestGameObject("Grain");
    }
    public override GameObject FindClosestDeliveryPoint()
    {
        return FindClosestGameObject("Mill");
    }

    public override void DoJob(Collider other, VillageResourceController resourceController)
    {
        if (Cargo <= CollectionCap && other.tag == "Grain")
        {
            Cargo += Time.deltaTime * 2;

        }
    }
    public override void Deposit(Collider other, VillageResourceController resourceController)
    {
        if (!GettingFood && other.tag == "Mill")
        {
            resourceController.StoredFood += (int)Cargo;
            Cargo = 0;
            DepostingCargo = false;

        }
    }
}
