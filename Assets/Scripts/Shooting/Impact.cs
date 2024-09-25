using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Impact : MonoBehaviour
{
    [SerializeField] AudioClip[] impactSounds;
    [SerializeField] ParticleSystem impactParticles;

    public void PlayImpact()
    {
        GetComponent<AudioSource>().PlayOneShot(impactSounds[Random.Range(0, impactSounds.Length)]);
        var emitParams = new ParticleSystem.EmitParams();
        impactParticles.Emit(emitParams, 15);
    }
}
