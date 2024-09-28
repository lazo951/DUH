using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTemplate", menuName = "Scriptable Objects/EnemyTemplate")]
public class EnemyTemplate : ScriptableObject
{
    public string enemyName;
    [TextArea(3, 5)] public string enemyDescription;

    [Header("Enemy Stats")]
    public float startHealth;

    [Header("Damage Effects")]
    public Color glowEffectColor;
    public float glowEffectDuration;
    public float glowEffectStrength;
}
