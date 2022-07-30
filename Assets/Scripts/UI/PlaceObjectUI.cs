using UnityEngine;
using UnityEngine.UI;

public class PlaceObjectUI : MonoBehaviour
{
    [SerializeField] private GameObject buttonPlaceObject;
    [SerializeField] private GameObject buttonControlObject;

    private void Start()
    {
        if(GameManager.Instance.usingDyslexic) SetDyslexic();
    }

    public void ShowPlaceObject()
    {
        buttonPlaceObject.SetActive(true);
    }
    
    public void HidePlaceObject()
    {
        buttonPlaceObject.SetActive(false);
    }

    public void ShowControlObject()
    {
        buttonControlObject.SetActive(true);
    }
    
    public void HideControlObject()
    {
        buttonControlObject.SetActive(false);
    }

    private void SetDyslexic()
    {
        ShowPlaceObject();
        ShowControlObject();

        Text[] allText = GetComponentsInChildren<Text>();
        GameManager.Instance.FontsToDyslexic(allText);
        
        HidePlaceObject();
        HideControlObject();
    }
}
