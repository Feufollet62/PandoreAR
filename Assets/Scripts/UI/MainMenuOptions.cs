using UnityEngine;
using UnityEngine.UI;

public class MainMenuOptions : MonoBehaviour
{
    [SerializeField] private GameObject menuMain;
    [SerializeField] private GameObject menuOptions;

    [SerializeField] private Toggle toggleDyslexic;

    private GameManager gm;
    private Text[] allText;

    private void Start()
    {
        gm = GameManager.Instance;
        CheckDyslexic();
    }

    private void CheckDyslexic()
    {
        // GetComponentsInChildren only works if the gameobject with a text component is active
        menuMain.SetActive(true);
        menuOptions.SetActive(true);

        allText = GetComponentsInChildren<Text>();
        print(allText.Length);
        if(gm.usingDyslexic) gm.FontsToDyslexic(allText);
        
        menuOptions.SetActive(false);
    }
    
    public void OpenOptions()
    {
        menuOptions.SetActive(true);
        toggleDyslexic.isOn = PlayerPrefs.GetInt("Using Dyslexic") == 1;
        
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
            gm.usingDyslexic = true;
            gm.FontsToDyslexic(allText);
        }
        else
        {
            PlayerPrefs.SetInt("Using Dyslexic", 0);
            gm.usingDyslexic = false;
            gm.LoadMainMenu(); // There is probably a better way but this works
        }
    }
}
