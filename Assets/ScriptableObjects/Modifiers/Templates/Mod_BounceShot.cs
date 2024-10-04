using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BounceShot", menuName = "Scriptable Objects/Modifiers/BounceShot")]
public class Mod_BounceShot : Mod_Base
{
    public float spread;
    public int maxBounces;
    public List<Mod_BounceShot> bounceShotFamily = new List<Mod_BounceShot>();

    public override void PermanentModifyWeapon()
    {
        foreach(Mod_BounceShot md in  bounceShotFamily) 
        {
            if (modForGun.ModifiersColission.Contains(md))
            {
                if(maxBounces > md.maxBounces)
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

        Vector3 spreadDirection = normal + Vector3.up * Random.Range(-spread, spread) + Vector3.right * Random.Range(-spread, spread);
        Quaternion rot = Quaternion.LookRotation(spreadDirection);

        Transform bullet = MainManager.Pooling.TakeBullet(firedFromGun.isPlayerGun);
        bullet?.GetComponent<Bullet>().StartBullet(impactPoint + spreadDirection, rot, firedFromGun, bounceCounter);
    }
}
