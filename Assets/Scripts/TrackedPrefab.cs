using UnityEngine;

public class TrackedPrefab : MonoBehaviour
{
    // This script lets the user go through different 3D models by swiping left or right on their phone.
    // Users can also scale objects up/down by pinching.

    [SerializeField] private GameObject[] prefabArray;
    private GameObject currentGameObject;
    private int currentPrefabIndex = 0;
    
    // Touch controls
    [Range(0,1)] [SerializeField] private float swipeTolerance = .05f;
    private Vector2 swipeStartPos;
    private Vector2 swipeDirection;
    
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
            // Pinch behavior goes here
        }
    }

    private void ProcessSwipe()
    {
        // Swipe too small; ignore it (Should be scaled with screen resolution)
        if(swipeDirection.magnitude < swipeTolerance * Screen.width) return;
        
        // Result is 1 if swipe is perfectly to the right, -1 if perfectly to the left
        float similarToRight = Vector2.Dot(Vector2.right, swipeDirection);
        
        if(similarToRight > 0) NextObject();
        else PreviousObject();
    }

    private void NextObject()
    {
        currentPrefabIndex++;
        if (currentPrefabIndex < prefabArray.Length - 1) currentPrefabIndex = 0; // Avoid overflow

        LoadObject();
    }
    private void PreviousObject()
    {
        currentPrefabIndex--;
        if (currentPrefabIndex < 0) currentPrefabIndex = prefabArray.Length - 1; // Avoid overflow

        LoadObject();
    }

    private void LoadObject()
    {
        if(currentGameObject) Destroy(currentGameObject);
        
        currentGameObject = Instantiate(prefabArray[currentPrefabIndex], transform);
    }
}
