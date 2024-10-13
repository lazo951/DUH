using UnityEngine;

[CreateAssetMenu(fileName = "WeaponReloadSpeed", menuName = "Scriptable Objects/Modifiers/WeaponReloadSpeed")]
public class Mod_ReloadSpeed : Mod_Base
{
    public float speedChange;

    public override void PermanentModifyWeapon()
    {
        modForGun.reloadSpeed += speedChange;
        modForGun.reloadSpeed = Mathf.Clamp(modForGun.reloadSpeed, 0.5f, 5f);

        Gun_Base scr = MainManager.Shooting.guns[modForGun].GetComponent<Gun_Base>();
        scr.UpdateAnimationReload();
    }
}
