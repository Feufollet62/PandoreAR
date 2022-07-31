using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Light))]
public class ARLightManager : MonoBehaviour
{
    // This code is mostly from AR samples provided by Unity
    // https://github.com/Unity-Technologies/arfoundation-samples/tree/4.2
    
    private ARCameraManager arCam;

    private Light light;

    private float? brightness;
    private float? colorTemperature;
    private Color? colorCorrection;
    
    private Vector3? mainLightDirection;
    private Color? mainLightColor;
    private float? mainLightIntensity; // In lumens

    private SphericalHarmonicsL2? sphericalHarmonics;

    private void Start()
    {
        arCam = FindObjectOfType<ARCameraManager>();
        light = GetComponent<Light>();
    }
    
    private void OnEnable()
    {
        arCam.frameReceived += FrameChanged;
    }
    
    private void FrameChanged(ARCameraFrameEventArgs args)
        {
            if (args.lightEstimation.averageBrightness.HasValue)
            {
                brightness = args.lightEstimation.averageBrightness.Value;
                light.intensity = brightness.Value;
            }
            else
            {
                brightness = null;
            }

            if (args.lightEstimation.averageColorTemperature.HasValue)
            {
                colorTemperature = args.lightEstimation.averageColorTemperature.Value;
                light.colorTemperature = colorTemperature.Value;
            }
            else
            {
                colorTemperature = null;
            }

            if (args.lightEstimation.colorCorrection.HasValue)
            {
                colorCorrection = args.lightEstimation.colorCorrection.Value;
                light.color = colorCorrection.Value;
            }
            else
            {
                colorCorrection = null;
            }
            
            if (args.lightEstimation.mainLightDirection.HasValue)
            {
                mainLightDirection = args.lightEstimation.mainLightDirection;
                light.transform.rotation = Quaternion.LookRotation(mainLightDirection.Value);
            }
            else
            {
                mainLightDirection = null;
            }

            if (args.lightEstimation.mainLightColor.HasValue)
            {
                mainLightColor = args.lightEstimation.mainLightColor;
                light.color = mainLightColor.Value;
            }
            else
            {
                mainLightColor = null;
            }

            if (args.lightEstimation.mainLightIntensityLumens.HasValue)
            {
                mainLightIntensity = args.lightEstimation.mainLightIntensityLumens;
                light.intensity = mainLightIntensity.Value;
            }
            else
            {
                mainLightIntensity = null;
            }

            if (args.lightEstimation.ambientSphericalHarmonics.HasValue)
            {
                sphericalHarmonics = args.lightEstimation.ambientSphericalHarmonics;
                RenderSettings.ambientMode = AmbientMode.Skybox;
                RenderSettings.ambientProbe = sphericalHarmonics.Value;
            }
            else
            {
                sphericalHarmonics = null;
            }
        }
}