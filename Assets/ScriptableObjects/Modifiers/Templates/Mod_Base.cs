using UnityEngine;

public abstract class Mod_Base : ScriptableObject
{
    public ModType modType;
    public ModWeaponType weaponStage;
    public ModPlayerType playerType;
    public GunTemplate modForGun;

    public virtual void InitialModifyWeapon()
    {
        //
    }

    public virtual void ModifyWeaponColission(Vector3 point)
    {
        //
    }

    public virtual void ModifyWeaponDuring(Transform callObject)
    {
        //
    }

    public virtual void InitialModifyPlayer()
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
