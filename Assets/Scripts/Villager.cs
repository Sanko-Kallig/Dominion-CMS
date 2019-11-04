using UnityEngine;
using System.Collections;

public class Villager 
{
    public float Hitpoints;
    public float Food;
    public float Comfort;
    public enum Job { Harvester, Woodcutter};
    public Job VillagerJob;

    public Villager(float hitpoints, float food, float comfort)
    {
        Hitpoints = hitpoints;
        Food = food;
        Comfort = comfort;
    }
}
