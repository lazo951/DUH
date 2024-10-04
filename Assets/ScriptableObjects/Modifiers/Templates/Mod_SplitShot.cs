using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SplitShot", menuName = "Scriptable Objects/Modifiers/SplitShot")]
public class Mod_SplitShot : Mod_Base
{
    public float spread;
    public int numberOfSplits;
    public int maxBounces;
    public List<Mod_SplitShot> splitShotFamily = new List<Mod_SplitShot>();

    public override void PermanentModifyWeapon()
    {
        foreach (Mod_SplitShot md in splitShotFamily)
        {
            if (modForGun.ModifiersColission.Contains(md))
            {
                if (numberOfSplits > md.numberOfSplits)
                {
                    modForGun.ModifiersColission.Remove(md);
                }
                else
                {
                    modForGun.ModifiersColission.Remove(this);
                }
            }
        }
    }

    public override void ModifyWeaponColission(GameObject hitObject, Vector3 normal, Vector3 impactPoint, GunTemplate firedFromGun, int bounceCounter)
    {
        if (bounceCounter > maxBounces)
            return;

        for (int i = 0; i < numberOfSplits; i++)
        {
            Vector3 spreadDirection = normal + Vector3.up * Random.Range(-spread, spread) + Vector3.right * Random.Range(-spread, spread);
            Quaternion rot = Quaternion.LookRotation(spreadDirection);

            Transform bullet = MainManager.Pooling.TakeBullet(firedFromGun.isPlayerGun);
            bullet?.GetComponent<Bullet>().StartBullet(impactPoint + spreadDirection, rot, firedFromGun, bounceCounter);
        }
    }
}
