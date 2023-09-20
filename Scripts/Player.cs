using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI moneytmpro;
    [SerializeField] private Image healthBar;
    [Header("Stats")]
    public int maxHealth;
    public int health;
    public int defense;
    public int money;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        healthBar.fillAmount = (float)health / (float)maxHealth;

        moneytmpro.text = "Money: " + money;
        // Player Rotation
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));

        if (health <= 0) Die();
    }

    private void FixedUpdate()
    {
        
    }

    private void Die()
    {
        gameOverScreen.SetActive(true);
        Destroy(this.gameObject);
        
    }
}
