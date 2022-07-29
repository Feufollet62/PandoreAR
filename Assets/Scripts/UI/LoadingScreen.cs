using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private RectTransform loadIconTransform;

    [SerializeField] private float fadeSpeed = 1f;
    [SerializeField] private float loadRotationSpeed = 50f;
    
    private Image[] imagesAll;
    private Text[] textAll;

    private bool isDisplayed = false;
    private bool finishedFade = true;
    
    private void Start()
    {
        imagesAll = GetComponentsInChildren<Image>();
        textAll = GetComponentsInChildren<Text>();
    }

    private void Update()
    {
        ManageFade();

        if(!isDisplayed) return;
        // Simple rotation around Y axis
        loadIconTransform.Rotate(Vector3.forward, -loadRotationSpeed * Time.deltaTime);
    }

    private void ManageFade()
    {
        //if (finishedFade) return;
        
        // Might not be the best in terms of performance
        float targetAlpha = 1f;
        targetAlpha = isDisplayed ? 1 : 0;
        
        foreach (Image image in imagesAll)
        {
            Color newColor = image.color;
            newColor.a = Mathf.Lerp(newColor.a, targetAlpha, fadeSpeed);

            image.color = newColor;
        }
        
        foreach (Text text in textAll)
        {
            Color newColor = text.color;
            newColor.a = Mathf.Lerp(newColor.a, targetAlpha, fadeSpeed);

            text.color = newColor;
        }
    }

    public void FadeIn()
    {
        isDisplayed = true;
    }
    
    public void FadeOut()
    {
        isDisplayed = false;
    }
}
