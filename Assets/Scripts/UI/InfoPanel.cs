using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private Text title;
    [SerializeField] private Image image;
    [SerializeField] private Text desc;

    private void Start()
    {
        Close();
    }

    public void LoadUI(ARObject arObject)
    {
        title.text = arObject.title;
        image.sprite = arObject.sprite;
        desc.text = arObject.description;

        parent.SetActive(true);
    }

    public void Close()
    {
        parent.SetActive(false);
    }
}
