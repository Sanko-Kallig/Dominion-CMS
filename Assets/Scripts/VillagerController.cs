
using UnityEngine;
using UnityEngine.AI;

public class VillagerController : MonoBehaviour
{
    public Villager CurrentVillager;
    public NavMeshAgent agent;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        CurrentVillager = new Villager(100, 100, 100);
        CurrentVillager.VillagerJob = Villager.Job.Harvester;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
