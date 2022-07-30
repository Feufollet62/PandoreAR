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
    public bool usingDyslexic = false;
    
    private void Awake()
    {
        SingletonCheck();
        LoadPrefs();
        DontDestroyOnLoad(loadingScreen);
    }
    
    private void SingletonCheck()
    {
        if(Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void LoadPrefs()
    {
        // If no key is found, create it (Default is false
        if (!PlayerPrefs.HasKey("Using Dyslexic")) PlayerPrefs.SetInt("Using Dyslexic", 0);
        
        if (PlayerPrefs.GetInt("Using Dyslexic") == 1) usingDyslexic = true;
    }

    public void FontsToDyslexic(Text[] texts)
    {
        // Changes all fonts to OpenDyslexic
        
        foreach (Text text in texts)
        {
            text.font = openDyslexic;
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName, float addedLoadTime) 
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