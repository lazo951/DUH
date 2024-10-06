using UnityEngine;

[CreateAssetMenu(fileName = "BulletDuration", menuName = "Scriptable Objects/Modifiers/BulletDuration")]
public class Mod_Duration : Mod_Base
{
    public float durationChange;

    public override void PermanentModifyWeapon()
    {
        modForGun.duration += durationChange;
        modForGun.duration = Mathf.Clamp(modForGun.duration, 0.1f, 20f);
    }
}
