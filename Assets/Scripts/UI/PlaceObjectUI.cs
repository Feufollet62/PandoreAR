using UnityEngine;

public class PlaceObjectUI : MonoBehaviour
{
    [SerializeField] private GameObject buttonPlaceObject;
    [SerializeField] private GameObject buttonControlObject;

    public void ShowPlaceObject()
    {
        buttonPlaceObject.SetActive(true);
    }
    
    public void HidePlaceObject()
    {
        buttonPlaceObject.SetActive(false);
    }
}
