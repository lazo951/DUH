using UnityEngine;

[CreateAssetMenu(fileName = "AllTheBullets", menuName = "Scriptable Objects/Modifiers/AllTheBullets")]
public class Mod_AllTheBullets : Mod_Base
{
    public int percentChance;

    public override void ModifyWeaponShoot(Transform spawnPos, GameObject gunShooting)
    {
        float ran = Random.Range(0f, 1f);
        if(ran < (float)percentChance /100f)
        {
            gunShooting.GetComponent<Gun_Base>().bulletsInMagazine = 0;
            MainManager.Shooting.UIAmmo();
        }
    }
}
