using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static Manager_Pooling Pooling;
    public static Manager_Player Player;
    public static Manager_Effects Effects;

    private void Start()
    {
        Pooling = GetComponent<Manager_Pooling>();
        Player = GetComponent<Manager_Player>();
        Effects = GetComponent<Manager_Effects>();

        Pooling.SetupValues();
        Player.SetupValues();
        Effects.SetupValues();
    }
}
