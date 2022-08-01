using UnityEngine;

public class InfoPoint : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
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
            print("zap");
            Ray ray = cam.ScreenPointToRay(thisTouch.position);
            if (Physics.Raycast(ray, 30f, mask))
            {
                print("touched");
            }
        }
    }
}