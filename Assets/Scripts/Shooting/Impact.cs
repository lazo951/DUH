using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    [SerializeField] AudioClip[] effectSounds;
    [SerializeField] ParticleSystem effectParticles;

    public void PlayImpact()
    {
        GetComponent<AudioSource>().PlayOneShot(effectSounds[Random.Range(0, effectSounds.Length)]);
        effectParticles.Play();
    }
}
