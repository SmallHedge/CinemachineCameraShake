using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource)), RequireComponent(typeof(CinemachineImpulseListener))]
public class CameraShake : MonoBehaviour
{
    [SerializeField, NoiseSettingsProperty] public NoiseSettings defaultShake;
    [SerializeField] private float globalForce = 1;

    private CinemachineImpulseListener listener;
    private CinemachineImpulseSource source;
    private static CameraShake instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        listener = GetComponent<CinemachineImpulseListener>();
        source = GetComponent<CinemachineImpulseSource>();
        source.m_DefaultVelocity = Vector3.back;
    }

    public static void Shake(CameraShakeProfile prof)
    {
        Shake(prof.duration, prof.amplitude, prof.frequency, prof.shakeType, prof.impulseShape);
    }

    public static void Shake(float duration = 1, float amplitude = 1, float frequency = 1, NoiseSettings shakeType = null, AnimationCurve impulseShape = null)
    {
        instance.listener.m_ReactionSettings.m_AmplitudeGain = amplitude;
        instance.listener.m_ReactionSettings.m_FrequencyGain = frequency;
        instance.listener.m_ReactionSettings.m_Duration = 0;
        if (shakeType) instance.listener.m_ReactionSettings.m_SecondaryNoise = shakeType;
        else instance.listener.m_ReactionSettings.m_SecondaryNoise = instance.defaultShake;

        instance.source.m_ImpulseDefinition.m_ImpulseDuration = duration;
        instance.source.m_ImpulseDefinition.m_ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Custom;
        if (impulseShape != null) instance.source.m_ImpulseDefinition.m_CustomImpulseShape = impulseShape;
        else instance.source.m_ImpulseDefinition.m_CustomImpulseShape = AnimationCurve.Constant(0, 1, 1);

        instance.source.GenerateImpulseWithForce(instance.globalForce);
    }
}
