using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIThink_Base : MonoBehaviour
{
    public EnemyTemplate enemyType;
    [HideInInspector]public float health;
    public ParticleSystem bloodParticles;
    Material mat;

    public LayerMask colideLayer;
    RaycastHit hit;

    AIMove_Base aiMove;
    AIAttack_Base aiAttack;

    public void SetupValues()
    {
        mat = GetComponent<MeshRenderer>().material;
        aiMove = GetComponent<AIMove_Base>();
        aiAttack = GetComponent<AIAttack_Base>();
        aiMove.SetupValues(enemyType.moveSpeed, enemyType.turnSpeed);
    }

    public void StartEnemy(Vector3 pos)
    {
        health = enemyType.startHealth;
        aiMove.SetPosition(pos);

        gameObject.SetActive(true);
        StartCoroutine(Think());
    }

    private IEnumerator Think()
    {
        yield return new WaitForSeconds(enemyType.thinkFrequency);

        float dist = Vector3.Distance(transform.position, MainManager.Player.player.position);
        if (dist > enemyType.preferredDistanceToPlayer)
        {
            Vector3 direction = MainManager.Player.player.position - transform.position;
            aiMove.MoveTo(transform.position + direction.normalized * 5);
        }
        else
        {
            if (Physics.Raycast(transform.position, MainManager.Player.player.position - transform.position, out hit, enemyType.preferredDistanceToPlayer, colideLayer, QueryTriggerInteraction.Ignore))
            {
                if(hit.collider.gameObject.CompareTag("Player"))
                {
                    aiMove.Stop();
                    aiMove.LookAt(MainManager.Player.player.position);
                    aiAttack.FireGun();
                }
                else
                {
                    aiMove.MoveTo(transform.position + Random.insideUnitSphere * 5f);
                }
            }
            else
            {
                aiMove.MoveTo(transform.position + Random.insideUnitSphere * 5f);
            }
        }

        StartCoroutine(Think());
    }

    public void Damage(float dmg, Vector3 impactPoint, Vector3 faceNormal)
    {
        health -= dmg;
        DamageEffect(impactPoint, faceNormal);

        if (health <= 0)
            gameObject.SetActive(false);
    }

    private void DamageEffect(Vector3 impactPoint, Vector3 faceNormal)
    {
        mat.SetColor("_Glow_Color", enemyType.glowEffectColor);
        DOVirtual.Float(0, enemyType.glowEffectStrength, enemyType.glowEffectDuration, val => mat.SetFloat("_Glow_Strength", val)).SetLoops(2, LoopType.Yoyo);

        MainManager.Effects.ShowHitMarker();

        if (faceNormal != Vector3.zero)
            bloodParticles.transform.rotation = Quaternion.LookRotation(faceNormal);

        bloodParticles.transform.position = impactPoint;
        bloodParticles.Play();
    }

    private void OnDisable()
    {
        MainManager.Pooling.ReturnEnemy(enemyType, transform);
    }
}
