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

    IEnumerator LoadSceneAsync(string sceneName) 
    {
        loadingScreen.FadeIn();

        yield return new WaitForSeconds(2f);
        
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while (!op.isDone)
        {
            yield return null;
        }

        print("done");
        loadingScreen.FadeOut();
    }
    
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