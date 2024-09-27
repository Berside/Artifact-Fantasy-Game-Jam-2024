using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    public Image healthBar; // The red rectangle (health bar)
    public Text healthText;         // The text that displays the health number
    public Transform player;


    private Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = player.GetComponent<Health>();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    // Update the health bar's size and text
    void UpdateHealthBar()
    {
        // Update the size of the red rectangle based on health percentage
        healthBar.fillAmount = Mathf.Clamp((float)playerHealth.currentHealth / playerHealth.maxHealth, 0, 1);

        // Update the health number to the left of the rectangle
        healthText.text = playerHealth.currentHealth.ToString();
    }

    // This method can be called when the player takes damage or heals
    public void ModifyHealth(int amount)
    {
        playerHealth.currentHealth = Mathf.Clamp(playerHealth.currentHealth + amount, 0, playerHealth.maxHealth);
        UpdateHealthBar();
    }
}
