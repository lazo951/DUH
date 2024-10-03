using UnityEngine;

[CreateAssetMenu(fileName = "WeaponFireRate", menuName = "Scriptable Objects/Modifiers/WeaponFireRate")]
public class Mod_FireRate : Mod_Base
{
    public int RPMChange;

    public override void PermanentModifyWeapon()
    {
        modForGun.rateOfFireRPM += RPMChange;
        modForGun.rateOfFireRPM = Mathf.Clamp(modForGun.rateOfFireRPM, 30, 900);
    }
}
