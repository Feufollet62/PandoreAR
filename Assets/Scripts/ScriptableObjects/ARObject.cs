using UnityEngine;

[CreateAssetMenu(fileName = "New AR Object", menuName = "ScriptableObjects/AR Object", order = 1)]
public class ARObject : ScriptableObject
{
    public GameObject prefab;
    
    public float initialScale;

    public string title;
    public string description;

    public Texture2D texture;
}