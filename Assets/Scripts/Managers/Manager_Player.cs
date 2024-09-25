using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Manager_Player : MonoBehaviour
{
    [Header("Stats")]
    public float health;
    public float maxHealth;

    [Header("UI")]
    public TMP_Text txtHealth;

    public void SetupValues()
    {
        UIHealth();
    }

    public void ChangeHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);

        UIHealth();
    }

    #region UI

    public void UIHealth()
    {
        txtHealth.text = health.ToString() + "/" + maxHealth.ToString();
    }

    #endregion
}
