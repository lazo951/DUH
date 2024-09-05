using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Manager_Player : MonoBehaviour
{
    [SerializeField] List<GunTemplate> allGuns = new List<GunTemplate>();

    [Header("Stats")]
    public float health;
    public float maxHealth;
    public Dictionary<GunTemplate, int> ammo = new Dictionary<GunTemplate, int>();

    public void SetupValues()
    {
        foreach(GunTemplate gun in allGuns)
        {
            ammo.Add(gun, gun.startAmmoInInventory);
        }
    }

    public void ChangeAmmo(GunTemplate gun, int amount)
    {
        ammo[gun] += amount;
        ammo[gun] = Mathf.Clamp(ammo[gun], 0, gun.maxAmmo);
    }

    public void ChangeHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
    }
}
