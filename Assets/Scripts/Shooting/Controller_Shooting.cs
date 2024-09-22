using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Controller_Shooting : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            MainManager.Player.activeGun?.Shoot(transform);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            MainManager.Player.activeGun?.Reload();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MainManager.Player.SwitchGunDirect(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MainManager.Player.SwitchGunDirect(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MainManager.Player.SwitchGunDirect(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            MainManager.Player.SwitchGunDirect(3);
        }
    }
}
