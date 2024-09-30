using UnityEngine;

public abstract class Mod_Base : ScriptableObject
{
    [TextArea(3, 5)] public string modDescription;

    public ModType modType;
    public ModPlayerType playerType;
    public ModWeaponType weaponStage;
    public GunTemplate modForGun;

    public virtual void PermanentModifyWeapon()
    {
        //
    }

    public virtual void ModifyWeaponShoot(Transform spawnPos)
    {
        //
    }

    public virtual void ModifyWeaponColission(GameObject hitObject, Vector3 normal, Vector3 impactPoint)
    {
        //
    }

    public virtual void ModifyWeaponFixedUpdate(Transform callObject)
    {
        //
    }

    public virtual void PermanentModifyPlayer()
    {
        //
    }

    public virtual void ModifyPlayer(Transform callObject) 
    {
        //
    }

    public void AttachModifier()
    {
        if(modType == ModType.weapon)
        {
            modForGun.AttachModifier(this, weaponStage);
        }
        else if(modType == ModType.player) 
        {
            //player
        }
        else
        {
            //player
            modForGun.AttachModifier(this, weaponStage);
        }
    }
}
