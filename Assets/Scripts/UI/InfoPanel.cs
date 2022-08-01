using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private Text title;
    [SerializeField] private Image image;
    [SerializeField] private Text desc;

    public void LoadUI(ARObject arObject)
    {
        title.text = arObject.title;
        image.sprite = arObject.sprite;
        desc.text = arObject.description;

        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
