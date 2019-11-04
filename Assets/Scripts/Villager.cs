using UnityEngine;
using System.Collections;

public class Villager 
{
    public float Hitpoints;
    public float Food;
    public float MaxFood;
    public float Comfort;
    public float Cargo;
    public float CollectionCap;
    public enum Job { Harvester, Woodcutter};
    public Job VillagerJob;
    public bool GettingFood;
    public bool DepostingCargo;

    public Villager(float hitpoints, float food, float comfort, float maxFood)
    {
        Hitpoints = hitpoints;
        Food = food;
        Comfort = comfort;
        MaxFood = maxFood;
    }
}
