using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    [SerializeField] AudioClip[] impactSounds;

    public void PlayImpact()
    {
        GetComponent<AudioSource>().PlayOneShot(impactSounds[Random.Range(0, impactSounds.Length)]);
        //play particle effect
    }
}
