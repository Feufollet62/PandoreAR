using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    
    public static GameManager Instance;

    private void Awake()
    {
        SingletonCheck();
    }
    
    private void SingletonCheck()
    {
        if(Instance) Destroy(gameObject);
        else Instance = this;
    }

    // Currently simple loading but could be improved with Async loading + a loading screen
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadImageTracking()
    {
        SceneManager.LoadScene("ImageTracking");
    }
    
    public void LoadObjectPlacement()
    {
        SceneManager.LoadScene("ObjectPlacement");
    }
}