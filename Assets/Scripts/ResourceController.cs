using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public float durablity;
    // Start is called before the first frame update
    void Start()
    {
        durablity = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(durablity <= 0)
        {
            Destroy(transform.gameObject);
        }
    }
}
