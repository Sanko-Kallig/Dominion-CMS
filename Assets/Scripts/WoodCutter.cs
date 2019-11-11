using UnityEngine;
using System.Collections;

public class WoodCutter : Villager
{
    public float CollectedWood;
    public WoodCutter(float hitpoints, float food, float comfort, float maxFood, Transform transform) : base(hitpoints, food, comfort, maxFood, transform)
    {
    }
    public override GameObject FindClosestResource()
    {
        return FindClosestGameObject("Tree");
    }
    public override GameObject FindClosestDeliveryPoint()
    {
        return FindClosestGameObject("WoodPile");
    }
    public override void DoJob(Collider other, ResourceController resourceController)
    {
        if (Cargo <= CollectionCap && other.tag == "Tree")
        {
            Cargo += Time.deltaTime * 2;

        }
    }
    public override void Deposit(Collider other, ResourceController resourceController)
    {
        if (!GettingFood && other.tag == "WoodPile")
        {
            resourceController.StoredFood += (int)Cargo;
            Cargo = 0;
            DepostingCargo = false;

        }
    }
}