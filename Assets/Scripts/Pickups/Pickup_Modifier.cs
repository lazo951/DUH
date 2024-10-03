using UnityEngine;

public class Pickup_Modifier : Pickup_Base
{
    public Mod_Base modifier;

    public override void PickedUp()
    {
        modifier.AttachModifier();

        if (modifier.modType == ModType.player)
            DisplayPickup(modifier.modName + " modifier");
        else
            DisplayPickup(modifier.modName + " for " + modifier.modForGun.gunName + " modifier");

        Destroy(gameObject);
    }
}
