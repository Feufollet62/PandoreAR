using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject objectToPlace;
    
    private ARRaycastManager arRay;
    private Pose placementPose;

    private bool isValid;

    private void Start()
    {
        arRay = FindObjectOfType<ARRaycastManager>();
    }

    private void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (isValid && Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        }
    }

    private void UpdatePlacementPose()
    {
        Vector2 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(.5f, .5f));
        var hits = new List<ARRaycastHit>();
        arRay.Raycast(screenCenter, hits, TrackableType.Planes);

        isValid = hits.Count != 0;
        
        if (isValid)
        {
            placementPose = hits[0].pose;

            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camForwardProjected = new Vector3(camForward.x, 0, camForward.z);

            placementPose.rotation = Quaternion.LookRotation(camForwardProjected);
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (isValid)
        {
            indicator.SetActive(true);
            indicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            indicator.SetActive(false);
        }
    }
}
