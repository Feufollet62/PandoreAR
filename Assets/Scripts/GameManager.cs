using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance { get; private set;}
    
    [SerializeField] private LoadingScreen loadingScreen;
    public bool usingDyslexic = false;

    private ARSession session;
    
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
        TextHelper.LoadFont();
        
        // If no key is found, create it (Default is false
        if (!PlayerPrefs.HasKey("Using Dyslexic")) PlayerPrefs.SetInt("Using Dyslexic", 0);
        
        if (PlayerPrefs.GetInt("Using Dyslexic") == 1) usingDyslexic = true;
    }

    private IEnumerator LoadSceneAsync(string sceneName, float addedLoadTime) 
    {
        loadingScreen.FadeIn();

        // Artificially increase load times; can be better for UX.
        // It would be weird for the loading screen to flash for .01 seconds and immediately disappear...
        yield return new WaitForSeconds(addedLoadTime); 
        
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        // Wait for loading to be done and for ar session to be initialized or tracking (has camera feed in some way)
        while (!op.isDone)
        {
            //print(ARSession.state.ToString());
            
            /*if (!(ARSession.state == ARSessionState.SessionInitializing ||
                  ARSession.state == ARSessionState.SessionTracking))
            {
                yield return null;
            }*/
            
            yield return null;
        }
        
        loadingScreen.FadeOut();
    }

    public void AssignARSession(ARSession newSession)
    {
        session = newSession;
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