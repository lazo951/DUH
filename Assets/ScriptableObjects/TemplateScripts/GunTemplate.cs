using UnityEngine;

public enum GunShootype { rigidbody, hitscan}

[CreateAssetMenu(fileName = "GunTemplate", menuName = "Scriptable Objects/GunTemplate")]
public class GunTemplate : ScriptableObject
{
    public string gunName;
    [TextArea(3,5)] public string gunDescription;

    [Header("Weapon Stats")]
    //public GunShootype gunType;
    public float reloadSpeed;
    public float rateOfFire;
    public float recoilStrength;

    [Header("Bullet Stats")]
    public float damage;
    public float speed;
    public float size;
    public float mass;
    public float drag;
    public float duration;

    [Header("Ammo Stats")]
    public int magazineSize;
    public int startAmmoInInventory;
    public int maxAmmo;

    [Header("Proximity Check Settings")]
    public LayerMask proximityCollisionMask;
    public float proximityRadius;
    public float bulletSpawnDistance;
}
