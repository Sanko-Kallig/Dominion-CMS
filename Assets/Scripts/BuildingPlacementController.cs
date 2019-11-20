﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacementController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> placeableObjectPrefabs;

    [SerializeField]
    private LayerMask placeableObjectTerrain;

    [SerializeField]
    private KeyCode VillageHouse = KeyCode.A;
    [SerializeField]
    private KeyCode GrainField = KeyCode.A;

    private GameObject currentPlaceableObject;

    private float mouseWheelRotation;

    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            //RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    public void HandleButton(string buildingName)
    {
        switch(buildingName)
        {
            case "Fireplace":
                currentPlaceableObject = Instantiate(placeableObjectPrefabs[0]);
                break;
            case "House":
                currentPlaceableObject = Instantiate(placeableObjectPrefabs[1]);
                break;
            case "Grainfield":
                currentPlaceableObject = Instantiate(placeableObjectPrefabs[2]);
                break;
            case "Woodpile":
                currentPlaceableObject = Instantiate(placeableObjectPrefabs[3]);
                break;
            case "Mill":
                currentPlaceableObject = Instantiate(placeableObjectPrefabs[4]);
                break;
        }
    }

    private void HandleNewObjectHotkey()
    {
        if (Input.GetKeyDown(GrainField))
        {
            if (currentPlaceableObject != null)
            {
                Destroy(currentPlaceableObject);
            }
            else
            {
                currentPlaceableObject = Instantiate(placeableObjectPrefabs[1]);
            }
        }
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, placeableObjectTerrain))
        {
            if(hitInfo.transform.GetChildCount() > 0)
            {
                currentPlaceableObject.transform.position = hitInfo.transform.GetChild(0).position;
            }
            //currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    //private void RotateFromMouseWheel()
    //{
    //    mouseWheelRotation += Input.mouseScrollDelta.y;
    //    currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    //}

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject = null;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(currentPlaceableObject);
            currentPlaceableObject = null;
        }
    }
}
