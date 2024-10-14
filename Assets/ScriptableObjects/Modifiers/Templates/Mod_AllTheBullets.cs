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
            Gun_Base scriptGun = gunShooting.GetComponent<Gun_Base>();

            for(int i = 0; i < scriptGun.bulletsInMagazine; i++)
            {
                Vector3 pos = spawnPos.position + spawnPos.right * Random.Range(-0.05f, 0.05f) + spawnPos.up * Random.Range(-0.05f, 0.05f);
                scriptGun.CheckProximity(pos, spawnPos);
            }

            scriptGun.bulletsInMagazine = 0;
            MainManager.Shooting.UIAmmo();
        }
    }
}
