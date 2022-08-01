using UnityEngine;

[CreateAssetMenu(fileName = "New AR Object", menuName = "ScriptableObjects/AR Object", order = 1)]
public class ARObject : ScriptableObject
{
    public GameObject prefab;
    
    public float initialScale = 1f;
    public float minScale = .5f;
    public float maxScale = 2f;

    public string title;
    public string description;

    public Sprite sprite;
}