using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSpread", menuName = "Scriptable Objects/Modifiers/WeaponSpread")]
public class Mod_Spread : Mod_Base
{
    public float spreadChange;

    public override void PermanentModifyWeapon()
    {
        modForGun.spread += spreadChange;
        modForGun.spread = Mathf.Clamp(modForGun.spread, 0.01f, 5f);
    }
}
