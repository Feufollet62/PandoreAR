using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;
    
    [SerializeField] private LoadingScreen loadingScreen;

    private void Awake()
    {
        SingletonCheck();
        DontDestroyOnLoad(loadingScreen);
    }
    
    private void SingletonCheck()
    {
        if(Instance) Destroy(gameObject);
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
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