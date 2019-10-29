using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {
    public GameObject Unit;
    Material OriginalMat;
	// Use this for initialization
	void Start () { 
        OriginalMat = transform.gameObject.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject GetUnit()
    {
        if(Unit != null)
        {
            return Unit;
        }
            return null;
    }

    public void RemoveUnit()
    {
        Unit = null;
    }

    public Material GetOrgMat()
    {
        return OriginalMat;
    }


    //public void SetUnit(GameObject obj)
    //{
    //    Unit = obj;
    //    Unit.GetComponent<UnitScript>().gridLoc = this.transform.name;
    //    Unit.transform.position = new Vector3(transform.position.x, Unit.transform.position.y, transform.position.z);
    //    //Unit.GetComponent<UnitScript>().setMove(true);
        
    //}

}
