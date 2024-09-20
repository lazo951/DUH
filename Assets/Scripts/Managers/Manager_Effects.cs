using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Effects : MonoBehaviour
{
    [SerializeField] Controller_Crosshair scriptCrosshair;
    //[SerializeField] AudioSource weaponAudio;

    public void SetupValues()
    {
        //
    }

    public void AnimateCrosshair()
    {
        scriptCrosshair.ToggleShooting();
    }

    public void ShowHitMarker()
    {
        scriptCrosshair.ToggleHitmarker();
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
