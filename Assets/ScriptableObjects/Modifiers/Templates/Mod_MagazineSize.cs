using UnityEngine;

[CreateAssetMenu(fileName = "WeaponMagazineSize", menuName = "Scriptable Objects/Modifiers/WeaponMagazineSize")]
public class Mod_MagazineSize : Mod_Base
{
    public int sizeChange;

    public override void PermanentModifyWeapon()
    {
        modForGun.magazineSize += sizeChange;
        modForGun.magazineSize = Mathf.Clamp(modForGun.magazineSize, 1, 100);
    }
}
