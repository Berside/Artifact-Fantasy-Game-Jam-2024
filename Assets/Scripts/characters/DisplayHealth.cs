using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    public RectTransform healthBar; // The red rectangle (health bar)
    public Text healthText;         // The text that displays the health number
    public Transform player;
    public Toggle toggle;


    private Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = player.GetComponent<Health>();
        toggle.onValueChanged.AddListener(ToggleHealthBar);
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    void ToggleHealthBar(bool isGodmode)
    {
        playerHealth.set_godmode(isGodmode);
    }

    // Update the health bar's size and text
    void UpdateHealthBar()
    {
        // Update the size of the red rectangle based on health percentage
        float healthPercentage = (float)playerHealth.currentHealth / playerHealth.maxHealth;
        healthBar.localScale = new Vector3(healthPercentage, 1f, 1f);

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
