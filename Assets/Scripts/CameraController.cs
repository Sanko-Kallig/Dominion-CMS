﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject CameraObject;
    public GameObject Camera_;
    public Camera RayCamera;
    private VillagerController villagerController;
    private bool VillagerSelected;
    // Use this for initialization
    void Start()
    {
        RayCamera = Camera.main;
        VillagerSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = RayCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (VillagerSelected)
                {
                    villagerController.agent.SetDestination(hit.point);
                }
                if (hit.transform.tag == "Player" && !VillagerSelected)
                {
                    villagerController = hit.transform.gameObject.GetComponent<VillagerController>();
                    VillagerSelected = true;

                }

                //agent.SetDestination(hit.point);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            VillagerSelected = false;
        }
    }
        void FixedUpdate()
        {
            float xAxisValue = Input.GetAxis("Horizontal");
            float zAxisValue = Input.GetAxis("Vertical");
            float yAxisValue = Input.GetAxis("Mouse ScrollWheel");
            if (CameraObject != null)
            {
                CameraObject.transform.Translate(new Vector3(xAxisValue, 0, zAxisValue));

                if (CameraObject.transform.localPosition.y > 0 || CameraObject.transform.localPosition.y == 0 && yAxisValue > 0)
                {
                    Camera_.transform.Rotate(new Vector3(yAxisValue * 20f, 0, 0));
                    CameraObject.transform.Translate(new Vector3(0, yAxisValue * 20f, 0));
                }
            }
            if (Input.GetKey(KeyCode.Q))
            {
                CameraObject.transform.Rotate(new Vector3(0, -1.5f, 0));
            }
            if (Input.GetKey(KeyCode.E))
            {
                CameraObject.transform.Rotate(new Vector3(0, 1.5f, 0));
            }
        }
    }
