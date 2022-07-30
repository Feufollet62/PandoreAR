using UnityEngine;
using UnityEngine.UI;

public static class TextHelper
{
    private static Font _openDyslexic;
    private static bool fontIsLoaded = false;

    public static void LoadFont()
    {
        if (fontIsLoaded) return;
        
        // Look into using adressables instead of this, potential performance gain
        fontIsLoaded = true;
        _openDyslexic = Resources.Load<Font>("Fonts/OpenDyslexic");
    }
    
    public static void FontsToDyslexic(Text[] texts)
    {
        // Changes all fonts to OpenDyslexic
        if(!fontIsLoaded) return;

        foreach (Text text in texts)
        {
            text.font = _openDyslexic;
        }
    }
}