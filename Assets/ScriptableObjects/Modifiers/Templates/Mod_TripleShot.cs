using UnityEngine;

[CreateAssetMenu(fileName = "GunTripleshot", menuName = "Scriptable Objects/Modifiers/GunTripleshot")]
public class Mod_TripleShot : Mod_Base
{
    public float spacingX;
    public float spacingY;

    public override void ModifyWeaponShoot(Transform spawnPos, GameObject gunShooting)
    {
        Vector3 pos1 = spawnPos.position + spawnPos.right * spacingX + spawnPos.up * -spacingY;
        Vector3 pos2 = spawnPos.position + spawnPos.right * -spacingX + spawnPos.up * -spacingY;

        Gun_Base scriptGun = gunShooting.GetComponent<Gun_Base>();

        scriptGun.CheckProximity(pos1, spawnPos);
        scriptGun.CheckProximity(pos2, spawnPos);
    }
}
