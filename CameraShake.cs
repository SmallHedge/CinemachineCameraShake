//Author: Small Hedge
//Published: 19/06/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera), typeof(CinemachineImpulseSource), typeof(CinemachineImpulseListener))]
public class CameraShake : MonoBehaviour
{
    [NoiseSettingsProperty, SerializeField] private NoiseSettings defaultShake;
    [SerializeField] private float globalForce = 1;

    private CinemachineImpulseListener listener;
    private CinemachineImpulseSource source;
    public static CameraShake instance;

    void Start()
    {
        listener = GetComponent<CinemachineImpulseListener>();
        source = GetComponent<CinemachineImpulseSource>();
        instance = this;
    }

    public void Shake(CameraShakeSO SO)
    {
        Shake(SO.duration, SO.amplitude, SO.frequency, SO.shakeType, SO.impulseShape);
    }

    public void Shake(float duration = 1, float amplitude = 1, float frequency = 1, NoiseSettings shakeType = null, AnimationCurve impulseShape = null)
    {
        listener.m_ReactionSettings.m_AmplitudeGain = amplitude;
        listener.m_ReactionSettings.m_FrequencyGain = frequency;
        listener.m_ReactionSettings.m_Duration = 0;
        if (shakeType) listener.m_ReactionSettings.m_SecondaryNoise = shakeType;
        else listener.m_ReactionSettings.m_SecondaryNoise = defaultShake;

        source.m_ImpulseDefinition.m_ImpulseDuration = duration;
        source.m_ImpulseDefinition.m_ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Custom;
        if (impulseShape != null) source.m_ImpulseDefinition.m_CustomImpulseShape = impulseShape;
        else source.m_ImpulseDefinition.m_CustomImpulseShape = AnimationCurve.EaseInOut(0, 1, 1, 0);
        source.m_DefaultVelocity = Random.insideUnitCircle;

        source.GenerateImpulseWithForce(globalForce);
    }
}
