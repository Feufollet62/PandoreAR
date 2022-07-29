using UnityEngine;

public class MainMenuOptions : MonoBehaviour
{
    [SerializeField] private GameObject menuMain;
    [SerializeField] private GameObject menuOptions;
    
    public void OpenOptions()
    {
        menuOptions.SetActive(true);
        menuMain.SetActive(false);
    }

    public void CloseOptions()
    {
        menuOptions.SetActive(false);
        menuMain.SetActive(true);
    }

    public void ToggleDyslexic(bool trueFalse)
    {
        if (trueFalse)
        {
            PlayerPrefs.SetInt("Using Dyslexic", 1);
            GameManager.Instance.FontsToDyslexic();
        }
        else
        {
            PlayerPrefs.SetInt("Using Dyslexic", 0);
            GameManager.Instance.LoadMainMenu(); // There is probably a better way but this works
        }
    }
}
