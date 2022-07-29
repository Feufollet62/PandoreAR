using UnityEngine;
using UnityEngine.UI;

public class UIRotate : MonoBehaviour
{
    [SerializeField] private GameObject loadIconObject;

    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float loadRotationSpeed = 50f;
    
    private RectTransform tr;
    private Image[] imagesAll;
    private Text[] textAll;
    
    private void Start()
    {
        imagesAll = GetComponentsInChildren<Image>();
        textAll = GetComponentsInChildren<Text>();
        tr = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Simple rotation around Y axis
        tr.Rotate(Vector3.forward, -loadRotationSpeed * Time.deltaTime);
    }

    private void FadeIn(float duration)
    {
        foreach (Image image in imagesAll)
        {
            image.CrossFadeAlpha(1,duration,false);
        }
        
        foreach (Text text in textAll)
        {
            text.CrossFadeAlpha(1,duration,false);
        }
    }
    
    private void FadeOut(float duration)
    {
        foreach (Image image in imagesAll)
        {
            image.CrossFadeAlpha(0,duration,false);
        }
        
        foreach (Text text in textAll)
        {
            text.CrossFadeAlpha(0,duration,false);
        }
    }
}
