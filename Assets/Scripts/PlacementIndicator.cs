using UnityEngine;

public class PlacementIndicator : MonoBehaviour
{
    [SerializeField] private Transform arrow;

    [SerializeField] private AnimationCurve curve;
    
    private void Update()
    {
        float yValue = curve.Evaluate(Time.time);
        arrow.localPosition = new Vector3(0, yValue, 0);
    }
}
