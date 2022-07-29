using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;
    
    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private Font openDyslexic;
    private bool dyslexic = false;
    
    private void Awake()
    {
        SingletonCheck();
        LoadPrefs();
        DontDestroyOnLoad(loadingScreen);
    }
    
    private void SingletonCheck()
    {
        if(Instance) Destroy(gameObject);
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void LoadPrefs()
    {
        if (!PlayerPrefs.HasKey("Using Dyslexic"))
        {
            PlayerPrefs.SetInt("Using Dyslexic", 0);
        }
        
        dyslexic = PlayerPrefs.GetInt("Using Dyslexic") == 1; // 1 = true, 0 (and everything else) = false
    }

    private void FontsToDyslexic()
    {
        // Changes all text in the scene to OpenDyslexic
        Text[] allText = FindObjectsOfType<Text>();

        foreach (Text text in allText)
        {
            text.font = openDyslexic;
        }
    }

    IEnumerator LoadSceneAsync(string sceneName, float addedLoadTime) 
    {
        loadingScreen.FadeIn();

        // Artificially increase load times; can be better for UX.
        // It would be weird for the loading screen to flash for .01 seconds and immediately disappear...
        yield return new WaitForSeconds(addedLoadTime); 
        
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while (!op.isDone)
        {
            yield return null;
        }
        
        
        if (dyslexic) FontsToDyslexic();
        
        loadingScreen.FadeOut();
    }
    
    public void LoadMainMenu()
    {
        StartCoroutine(LoadSceneAsync("MainMenu", 1f));
    }

    public void LoadImageTracking()
    {
        StartCoroutine(LoadSceneAsync("ImageTracking", 2f));
    }
    
    public void LoadObjectPlacement()
    {
        StartCoroutine(LoadSceneAsync("ObjectPlacement", 2f));
    }
}