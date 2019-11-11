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
}
