using UnityEngine;

public class TrackedPrefab : MonoBehaviour
{
    // This script lets the user go through different 3D models by swiping left or right on their phone.
    // Users can also scale objects up/down by pinching.

    [SerializeField] private GameObject[] prefabArray;
    private int currentPrefab = 0;
    
    private void Start()
    {
        Instantiate(prefabArray[currentPrefab], transform);
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if(Input.touchCount == 0) return;
    }
}
