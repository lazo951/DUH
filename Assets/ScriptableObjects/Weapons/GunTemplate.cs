using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunTemplate", menuName = "Scriptable Objects/GunTemplate")]
public class GunTemplate : ScriptableObject
{
    public string gunName;
    [TextArea(3,5)] public string gunDescription;

    [Header("Weapon Stats")]
    public float reloadSpeed;
    public float rateOfFire;
    public float recoilStrength;

    [Header("Bullet Stats")]
    public float damage;
    public float speed;
    public float size;
    public float duration;
    public Vector3 forceOverLifetime;

    [Header("Ammo Stats")]
    public int magazineSize;
    public int startAmmoInInventory;
    public int maxAmmo;

    [Header("Proximity Check Settings")]
    public LayerMask proximityCollisionMask;
    public float proximityRadius;
    public float bulletSpawnDistance;

    [Header("Audio")]
    public AudioClip[] soundShooting;
    public AudioClip soundReload;
    public AudioClip soundEmpty;

    [Header("Modifiers")]
    public List<Mod_Base> ModifiersShoot = new List<Mod_Base>();
    public List<Mod_Base> ModifiersFixedUpdate = new List<Mod_Base>();
    public List<Mod_Base> ModifiersColission = new List<Mod_Base>();

    public void AttachModifier(Mod_Base mod, ModWeaponType stage)
    {
        if (ModifiersShoot.Contains(mod) || ModifiersFixedUpdate.Contains(mod) || ModifiersColission.Contains(mod))
            return;

        if (stage == ModWeaponType.start)
        {
            ModifiersShoot.Add(mod);
        }
        else if(stage == ModWeaponType.during)
        {
            ModifiersFixedUpdate.Add(mod);
        }
        else if(stage == ModWeaponType.colission)
        {
            ModifiersColission.Add(mod);
        }

        mod.PermanentModifyWeapon();
    }

    public void RemoveAllModifiers()
    {
        ModifiersShoot.Clear();
        ModifiersFixedUpdate.Clear();
        ModifiersColission.Clear();
    }
}
