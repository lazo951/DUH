using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Effects : MonoBehaviour
{
    [SerializeField] Controller_Crosshair scriptCrosshair;

    public void SetupValues()
    {
        //
    }

    public void AnimateCrosshair()
    {
        scriptCrosshair.ToggleShooting();
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
