using UnityEngine;
using System.Collections;

public class Harvester : Villager
{
    public float CollectedFood;
    public Harvester(float hitpoints, float food, float comfort, float maxFood) : base(hitpoints, food, comfort, maxFood)
    {
    }
}
