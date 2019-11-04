
using UnityEngine;
using UnityEngine.AI;

public class VillagerController : MonoBehaviour
{
    public Villager CurrentVillager;
    public NavMeshAgent agent;
    public Camera camera;
    public float Food;
    public float Cargo;
    public ResourceController resourceController;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        CurrentVillager = new Harvester(100, 40, 100, 100);
        CurrentVillager.GettingFood = false;
        CurrentVillager.DepostingCargo = false;
        CurrentVillager.VillagerJob = Villager.Job.Harvester;
        CurrentVillager.CollectionCap = 20;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentVillager.Food -= Time.deltaTime * 1;
        Food = CurrentVillager.Food;
        Cargo = CurrentVillager.Cargo;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
       if (CurrentVillager.Food <= 30 && !CurrentVillager.GettingFood)
        {
            CurrentVillager.GettingFood = true;
            agent.SetDestination(FindClosestMills().transform.position);
        }
       if(!CurrentVillager.GettingFood && !CurrentVillager.DepostingCargo && CurrentVillager.Cargo < CurrentVillager.CollectionCap && CurrentVillager.GetType().Name == "Harvester")
        {
                agent.SetDestination(FindClosestGrain().transform.position);
        }
       if(!CurrentVillager.GettingFood && !CurrentVillager.DepostingCargo && (int)CurrentVillager.Cargo == (int)CurrentVillager.CollectionCap && CurrentVillager.GetType().Name == "Harvester")
        {
                CurrentVillager.DepostingCargo = true;
                agent.SetDestination(FindClosestMills().transform.position);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!CurrentVillager.GettingFood && CurrentVillager.GetType().Name == "Harvester" && collision.collider.tag == "Grain")
        {
            if(CurrentVillager.Cargo <= CurrentVillager.CollectionCap)
            {
                CurrentVillager.Cargo += Time.deltaTime * 10;
                
            }
        }
        if (!CurrentVillager.GettingFood && CurrentVillager.GetType().Name == "Harvester" && collision.collider.tag == "Mill")
        {
            resourceController.StoredFood += (int)CurrentVillager.Cargo;
            CurrentVillager.Cargo = 0;
            CurrentVillager.DepostingCargo = false;

        }
        if (collision.collider.tag == "Mill" && CurrentVillager.GettingFood && CurrentVillager.Cargo <= CurrentVillager.CollectionCap)
        {
            float temp;
            temp = CurrentVillager.MaxFood - CurrentVillager.Food;
            resourceController.StoredFood -= (int)temp;
            CurrentVillager.Food += temp;
            CurrentVillager.GettingFood = false;
        }
    }

    public GameObject FindClosestMills()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Mill");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
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

    public GameObject FindClosestGrain()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Grain");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
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
