using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Effects : MonoBehaviour
{
    [SerializeField] CameraMovement scriptCamera;
    [SerializeField] Controller_Crosshair scriptCrosshair;
    [SerializeField] AudioSource hitmarkerAudio;

    public void SetupValues()
    {
        //
    }

    public void ShootEffects(float intensity)
    {
        scriptCamera.AddRecoil(intensity, 0.5f);
        scriptCrosshair.ToggleShooting((int)intensity);
    }

    public void ShowHitMarker()
    {
        scriptCrosshair.ToggleHitmarker();

        if(!hitmarkerAudio.isPlaying)
            hitmarkerAudio.Play();
    }

    public void CameraShake()
    {
        //
    }

    public void PlayerDamageIndicator()
    {
        //
    }
}
