using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NameChange", menuName = "Scriptable Objects/Modifiers/NameChange")]
public class Mod_NameChange : Mod_Base
{
    public string newName;

    public override void PermanentModifyWeapon()
    {
        modForGun.gunName = newName;
        MainManager.Shooting.UIAmmo();
    }
}
