using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTemplate", menuName = "Scriptable Objects/EnemyTemplate")]
public class EnemyTemplate : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab;

    [Header("Enemy Stats")]
    public float startHealth;
    public float moveSpeed;
    public float turnSpeed;

    [Header("AI")]
    public float thinkFrequency;
    public float preferredDistanceToPlayer;

    [Header("Damage Effects")]
    public Color glowEffectColor;
    public float glowEffectDuration;
    public float glowEffectStrength;
}
