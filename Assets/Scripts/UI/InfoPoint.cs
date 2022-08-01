using UnityEngine;

public class InfoPoint : MonoBehaviour
{
    [SerializeField] private ARObject arObject;
    [SerializeField] private LayerMask mask;
    
    private Camera cam;
    private InfoPanel infoPanel;

    private void Start()
    {
        cam = Camera.main;
        infoPanel = FindObjectOfType<InfoPanel>();
        infoPanel.Close();
    }

    private void Update()
    {
        transform.forward = cam.transform.forward;
        GetTouch();
    }

    private void GetTouch()
    {
        if (Input.touchCount == 0) return;
        
        Touch thisTouch = Input.GetTouch(0);

        if (thisTouch.phase == TouchPhase.Ended)
        {
            Ray ray = cam.ScreenPointToRay(thisTouch.position);
            if (Physics.Raycast(ray, 30f, mask))
            {
                infoPanel.LoadUI(arObject);
            }
        }
    }
}