using UnityEngine;

public class UIRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    
    private RectTransform tr;
    
    private void Start()
    {
        tr = GetComponent<RectTransform>();
    }

    private void Update()
    {
        tr.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
    }
}
