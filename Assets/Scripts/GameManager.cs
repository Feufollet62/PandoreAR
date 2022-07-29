using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;
    [SerializeField] private GameObject loadingScreen;

    private void Awake()
    {
        SingletonCheck();
    }
    
    private void SingletonCheck()
    {
        if(Instance) Destroy(gameObject);
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(loadingScreen);
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);
        
        while (!op.isDone)
        {
            yield return null;
        }
        
        loadingScreen.SetActive(false);
    }

    // Currently simple loading but could be improved with Async loading + a loading screen
    public void LoadMainMenu()
    {
        StartCoroutine(LoadSceneAsync("MainMenu"));
    }

    public void LoadImageTracking()
    {
        StartCoroutine(LoadSceneAsync("ImageTracking"));
    }
    
    public void LoadObjectPlacement()
    {
        StartCoroutine(LoadSceneAsync("ObjectPlacement"));
    }
}