using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
* Author: Hazel Wang Sim Yee
* Date: 15/6/2025
* Description: Script to manage player's health bar
*/


public class HealthBarBehaviour : MonoBehaviour
{
    // Set health bar values in the inspector
    public float Health, MaxHealth, Width, Height;

    [SerializeField]
    private RectTransform HealthBar;

    public void SetMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
    }

    public void SetHealth(float health) // adjust length of health bar based on player's health
    {
        Health = health;
        Debug.Log("Health set to: " + Health);
        Debug.Log("Max Health: " + MaxHealth);
        Debug.Log("HealthBar Width: " + Width);
        float newWidth = (Health / MaxHealth) * Width;
        Debug.Log("New HealthBar width: " + newWidth);
        HealthBar.sizeDelta = new Vector2(newWidth, Height);
    }
}

