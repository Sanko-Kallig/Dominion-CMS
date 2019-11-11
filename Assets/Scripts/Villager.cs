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
    public enum Job { Harvester, Woodcutter, StoneCutter};
    public Job VillagerJob;
    public bool GettingFood;
    public bool DepostingCargo;
    public Transform _transform;

    public Villager(float hitpoints, float food, float comfort, float maxFood, Transform transform)
    {
        Hitpoints = hitpoints;
        Food = food;
        Comfort = comfort;
        MaxFood = maxFood;
        _transform = transform;
    }

    public virtual GameObject FindClosestResource()
    {
        return default(GameObject);
    }
    public virtual GameObject FindClosestDeliveryPoint()
    {
        return default(GameObject);
    }

    public GameObject FindClosestFood()
    {
       return FindClosestGameObject("Mill");
    }
    public GameObject FindClosestGameObject(string target)
    {

        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(target);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = _transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
