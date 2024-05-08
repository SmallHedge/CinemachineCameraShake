using UnityEngine;
using Cinemachine;

[CreateAssetMenu(menuName = "Camera Shake/Camera Shake Profile", fileName = "New Profile")]
public class CameraShakeProfile : ScriptableObject
{
    [NoiseSettingsProperty] public NoiseSettings shakeType;
    public float amplitude = 1;
    public float frequency = 1;
    public float duration = 1;
    public AnimationCurve impulseShape = AnimationCurve.EaseInOut(0, 1, 1, 0);
}