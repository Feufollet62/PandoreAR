using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject objectToPlace;

    [Header("Variables")] 
    [SerializeField] private float rotationAngle = 45f;
    [SerializeField] private float scaleFactor = 2;

    [Header("UI")]
    [SerializeField] private GameObject buttonPlaceObject;
    [SerializeField] private GameObject buttonControlObject;
    
    private Camera mainCamera;
    private ARRaycastManager arRay;
    private Pose placementPose;

    private GameObject placedObject;
    
    private bool raycastIsValid;
    
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
            buttonControlObject.SetActive(true);
            raycastIsValid = false;
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
            buttonPlaceObject.SetActive(true);
            indicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            indicator.SetActive(false);
            buttonPlaceObject.SetActive(false);
        }
    }

    public void Scale(bool scaleUp)
    {
        // X, Y, and Z should all be the same
        float initialScale = placedObject.transform.localScale.x;
        float newScale = initialScale;
        
        
        if (scaleUp) // This is wrong: won't be uniform
        {
            newScale *= scaleFactor;
        }
        else
        {
            newScale *= 1/scaleFactor;
        }
        
        placedObject.transform.localScale = Vector3.one * newScale;
    }

    public void Rotate()
    {
        placedObject.transform.Rotate(0,rotationAngle,0);
    }

    public void Reset()
    {
        GameManager.Instance.LoadObjectPlacement();
    }
}
