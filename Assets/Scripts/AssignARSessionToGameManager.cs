using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AssignARSessionToGameManager : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.AssignARSession(FindObjectOfType<ARSession>());
    }
}
