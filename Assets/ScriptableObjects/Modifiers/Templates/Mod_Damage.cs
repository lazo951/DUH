using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDamage", menuName = "Scriptable Objects/Modifiers/WeaponDamage")]
public class Mod_Damage : Mod_Base
{
    public int percentChange;

    public override void PermanentModifyWeapon()
    {
        modForGun.damage *= (1f + (float)percentChange / 100f);
        modForGun.damage = Mathf.Clamp(modForGun.damage, 0, 1000);
    }
}
