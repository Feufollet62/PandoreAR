using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private RectTransform loadIconTransform;

    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float loadRotationSpeed = 50f;
    
    private Image[] imagesAll;
    private Text[] textAll;

    private bool isDisplayed = false;
    
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
        print("1");
        if (isDisplayed)
        {
            print("2");
            foreach (Image image in imagesAll)
            {
                image.CrossFadeAlpha(1,fadeDuration,false);
                print(image.color.a);
            }
        
            foreach (Text text in textAll)
            {
                text.CrossFadeAlpha(1,fadeDuration,false);
                print(text.color.a);
            }
        }
        else
        {
            foreach (Image image in imagesAll)
            {
                image.CrossFadeAlpha(0.00392156863f,fadeDuration,false);
            }
        
            foreach (Text text in textAll)
            {
                text.CrossFadeAlpha(0.00392156863f,fadeDuration,false);
            }
        }
    }

    public void FadeIn()
    {
        isDisplayed = true;
        
        // Dumb bug with CrossFadeAlpha; if initial alpha value is 0 it doesn't work...
        // 0.00392156863 is a value of 1 alpha in RGB
        foreach (Image image in imagesAll)
        {
            Color newColor = image.color;
            newColor.a = 0.00392156863f;
            
            image.color = newColor;
        }
        
        foreach (Text text in textAll)
        {
            Color newColor = text.color;
            newColor.a = 0.00392156863f;
            
            text.color = newColor;
        }
    }
    
    public void FadeOut()
    {
        isDisplayed = false;
        print(isDisplayed);
    }
}
