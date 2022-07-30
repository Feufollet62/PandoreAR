using UnityEngine;
using UnityEngine.UI;

public class TrackedPrefab : MonoBehaviour
{
    // This script lets the user go through different 3D models by swiping left or right on their phone.
    // Users can also scale objects up/down by pinching.
    
    [SerializeField] private ARObject[] objects;
    private GameObject currentGameObject;
    private int currentPrefabIndex = 0;
    
    // Touch controls
    [Range(0,1)] [SerializeField] private float swipeTolerance = .05f;
    
    // Swiping
    private Vector2 swipeStartPos;
    private Vector2 swipeDirection;
    
    //Pinching
    private float pinchInitialDistance;
    private float pinchInitialScale;
    
    private void Start()
    {
        LoadObject();
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if(Input.touchCount == 0) return;
        
        // Handle swipes
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                // Initial touch position
                case TouchPhase.Began:
                    swipeStartPos = touch.position;
                    break;

                // Compare current position to start to get direction
                case TouchPhase.Moved:
                    swipeDirection = touch.position - swipeStartPos;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    ProcessSwipe();
                    break;
            }
        }

        // Pinch
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0); 
            Touch touchOne = Input.GetTouch(1);

            // If a touch ends / is cancelled do nothing
            if(touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled
            || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled) 
            {
                return;
            }

            // User started pinching
            if(touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                // Track the initial values
                pinchInitialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                
                // Scale is gonna be uniform so just take x
                pinchInitialScale = transform.localScale.x;
            }

            // User is moving fingers or stationary
            else
            {
                float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                // If pinchInitialDistance too close to 0, it means it could be faulty input
                if (Mathf.Approximately(pinchInitialDistance, 0)) return;

                // Multiplication avoids negative scale
                float factor = currentDistance / pinchInitialDistance;
                transform.localScale = pinchInitialScale * factor * Vector3.one;

                ARObject currentObj = objects[currentPrefabIndex];
                
                // Clamp between min and max possible value
                if (transform.localScale.x < currentObj.minScale)
                {
                    transform.localScale = Vector3.one * currentObj.minScale;
                }
                if (transform.localScale.x > currentObj.maxScale)
                {
                    transform.localScale = Vector3.one * currentObj.maxScale;
                }
            }
        }
    }

    private void ProcessSwipe()
    {
        // Swipe too small; ignore it (Should be scaled with screen resolution)
        if(swipeDirection.magnitude < swipeTolerance * Screen.width) return;
        
        // Result is 1 if swipe is perfectly to the right, -1 if perfectly to the left
        float similarToLeft = Vector2.Dot(Vector2.left, swipeDirection);
        
        if(similarToLeft > 0) NextObject();
        else PreviousObject();
    }

    private void NextObject()
    {
        currentPrefabIndex++;
        if (currentPrefabIndex > objects.Length - 1) currentPrefabIndex = 0; // Avoid overflow

        LoadObject();
    }
    
    private void PreviousObject()
    {
        currentPrefabIndex--;
        if (currentPrefabIndex < 0) currentPrefabIndex = objects.Length - 1; // Avoid overflow

        LoadObject();
    }

    private void LoadObject()
    {
        // Destroy old object and load new one
        if(currentGameObject) Destroy(currentGameObject);
        
        currentGameObject = Instantiate(objects[currentPrefabIndex].prefab, transform);
        
        // Apply correct scale
        transform.localScale = Vector3.one * objects[currentPrefabIndex].initialScale;
    }
}
