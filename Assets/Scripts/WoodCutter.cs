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
}