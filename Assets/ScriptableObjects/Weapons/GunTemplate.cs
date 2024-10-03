using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunTemplate", menuName = "Scriptable Objects/GunTemplate")]
public class GunTemplate : ScriptableObject
{
    public GunTemplate baseGunValues;
    public string gunName;
    [TextArea(3,5)] public string gunDescription;

    [Header("Weapon Stats")]
    public float reloadSpeed;
    public int rateOfFireRPM;
    public float recoilStrength;

    [Header("Bullet Stats")]
    public float damage;
    public float speed;
    public float size;
    public float duration;
    public Vector3 forceOverLifetime;

    [Header("Effects")]
    public Gradient bulletTrailColor;
    public float bulletTrailDuration;

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
    public List<Mod_Base> ModifiersPickup = new List<Mod_Base>();
    public List<Mod_Base> ModifiersShoot = new List<Mod_Base>();
    public List<Mod_Base> ModifiersFixedUpdate = new List<Mod_Base>();
    public List<Mod_Base> ModifiersColission = new List<Mod_Base>();

    public void AttachModifier(Mod_Base mod, ModWeaponType stage)
    {
        if (ModifiersShoot.Contains(mod) || ModifiersFixedUpdate.Contains(mod) || ModifiersColission.Contains(mod) )
            return;

        if (stage == ModWeaponType.onPickup)
        {
            ModifiersPickup.Add(mod);
        }
        else if (stage == ModWeaponType.onShoot)
        {
            ModifiersShoot.Add(mod);
        }
        else if(stage == ModWeaponType.onUpdate)
        {
            ModifiersFixedUpdate.Add(mod);
        }
        else if(stage == ModWeaponType.onColission)
        {
            ModifiersColission.Add(mod);
        }

        mod.PermanentModifyWeapon();
    }

    public void ResetValues()
    {
        gunName = baseGunValues.gunName;
        gunDescription = baseGunValues.gunDescription;
        reloadSpeed = baseGunValues.reloadSpeed;
        rateOfFireRPM = baseGunValues.rateOfFireRPM;
        recoilStrength = baseGunValues.recoilStrength;
        damage = baseGunValues.damage;
        speed = baseGunValues.speed;
        size = baseGunValues.size;
        duration = baseGunValues.duration;
        forceOverLifetime = baseGunValues.forceOverLifetime;
        bulletTrailColor = baseGunValues.bulletTrailColor;
        bulletTrailDuration = baseGunValues.bulletTrailDuration;
        magazineSize = baseGunValues.magazineSize;
        startAmmoInInventory = baseGunValues.startAmmoInInventory;
        maxAmmo = baseGunValues.maxAmmo;
        proximityCollisionMask = baseGunValues.proximityCollisionMask;
        proximityRadius = baseGunValues.proximityRadius;
        bulletSpawnDistance = baseGunValues.bulletSpawnDistance;
        soundShooting = baseGunValues.soundShooting;
        soundReload = baseGunValues.soundReload;
        soundEmpty = baseGunValues.soundEmpty;

        ModifiersPickup.Clear();
        ModifiersShoot.Clear();
        ModifiersFixedUpdate.Clear();
        ModifiersColission.Clear();

        foreach(Mod_Base md in baseGunValues.ModifiersPickup)
        {
            ModifiersPickup.Add(md);
        }

        foreach (Mod_Base md in baseGunValues.ModifiersShoot)
        {
            ModifiersShoot.Add(md);
        }

        foreach (Mod_Base md in baseGunValues.ModifiersFixedUpdate)
        {
            ModifiersFixedUpdate.Add(md);
        }

        foreach (Mod_Base md in baseGunValues.ModifiersColission)
        {
            ModifiersColission.Add(md);
        }
    }
}
