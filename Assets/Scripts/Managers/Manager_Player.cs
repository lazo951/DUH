using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Manager_Player : MonoBehaviour
{
    public Transform player;
    private PlayerMovement movement;

    [Header("Stats")]
    public float health;
    public float maxHealth;
    public int jumpTimes = 1;
    public float speedModifier = 1;

    [Header("UI")]
    public TMP_Text txtHealth;

    [Header("Modifiers")]
    public List<Mod_Base> ModifiersPickup = new List<Mod_Base>();
    public float speedIncrement = 0.2f;

    private void Start()
    {
        movement = player.GetComponent<PlayerMovement>();
    }

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

    public void IncreasePlayerSpeed()
    {
        speedModifier += speedIncrement;
        SetPlayerSpeed();
    }

    public void SetPlayerSpeed()
    {
        movement.walkSpeed *= speedModifier;
        movement.runSpeed *= speedModifier;
        movement.crouchSpeed *= speedModifier;
        movement.jumpSpeed *= speedModifier;
    }

    public void IncreasePlayerJumpTimes()
    {
        jumpTimes++;
        SetPlayerJumpTimes();
    }

    public void SetPlayerJumpTimes()
    {
        movement.jumpTimes = jumpTimes;
        movement.jumpCounter = jumpTimes - 1;
    }

    public void AttachModifier(Mod_Base mod)
    {               
        ModifiersPickup.Add(mod);        
        mod.PermanentModifyPlayer();
    }

    #region UI

    public void UIHealth()
    {
        txtHealth.text = health.ToString() + "/" + maxHealth.ToString();
    }

    #endregion
}
