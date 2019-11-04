using UnityEngine;
using System.Collections;

public class WoodCutter : Villager
{
    public float CollectedWood;
    public WoodCutter(float hitpoints, float food, float comfort, float maxFood) : base(hitpoints, food, comfort, maxFood)
    {
    }
}
