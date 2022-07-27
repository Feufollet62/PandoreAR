using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject objectToPlace;

    [Header("UI")]
    [SerializeField] private Button buttonPlaceObject;
    [SerializeField] private GameObject buttonControlButtons;
    
    private Camera mainCamera;
    private ARRaycastManager arRay;
    private Pose placementPose;

    private bool raycastIsValid;

    private GameObject placedObject;

    private void Start()
    {
        mainCamera = Camera.main;
        arRay = FindObjectOfType<ARRaycastManager>();
    }

    private void Update()
    {
        // If object is already placed, don't do anything
        if(placedObject) return;
        
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        // All conditions are met: raycast is valid and user input detected
        if (raycastIsValid && Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            placedObject = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        }
    }

    private void UpdatePlacementPose()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        Vector2 screenCenter = mainCamera.ViewportToScreenPoint(new Vector3(.5f, .5f));

        // Raycast from screen center to look for planes
        arRay.Raycast(screenCenter, hits, TrackableType.Planes);
        
        raycastIsValid = hits.Count != 0;
        if (!raycastIsValid) return;
        
        // Get the pose of the first valid raycast
        placementPose = hits[0].pose;

        // Align with camera
        Vector3 camForward = mainCamera.transform.forward;
        Vector3 camForwardProjected = new Vector3(camForward.x, 0, camForward.z);

        placementPose.rotation = Quaternion.LookRotation(camForwardProjected);
    }

    private void UpdatePlacementIndicator()
    {
        // Makes indicator invisible if no valid surface is detected
        
        if (raycastIsValid)
        {
            indicator.SetActive(true);
            indicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    private void Reset()
    {
        GameManager.Instance.LoadObjectPlacement();
    }
}
