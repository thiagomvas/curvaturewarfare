using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public PlayerSide disabledSide;
    public GameObject disabledVFX;
    private ScreenShake ss;
    [SerializeField] public SoundManager sm;
    [SerializeField] public GameObject player;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject DeathVFX;
    public GameController gc;

    [Header("Stats")]
    [SerializeField] private int damage;
    [SerializeField] public float MaxHealth;
    [SerializeField] private int health;
    [SerializeField] public float speed;
    [SerializeField] public int moneyOnKill;
    [SerializeField] public int pointsOnKill;
    [SerializeField] private float shakeDur;
    [SerializeField] private float shakeMag;

    Rigidbody2D rb;
    void Start()
    {
        ss = Camera.main.GetComponent<ScreenShake>();
        health = (int)MaxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(disabledSide != null) disabledSide.canDoAction = false;
        if(disabledVFX != null) disabledVFX.SetActive(true);
        if(disabledVFX != null) disabledVFX.transform.position = disabledSide.transform.position;
        healthBar.fillAmount = (float)health / (float)MaxHealth;
        if (health <= 0) Die();
    }


    void FixedUpdate()
    {
        //rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            health -= other.GetComponent<Bullet>().damage;
        }
        if(other.CompareTag("Player"))
        {
            player.GetComponent<Player>().health -= damage;
            Die();

        }
    }

    private void Die()
    {
        if(disabledSide != null) disabledSide.canDoAction = true;
        if(disabledVFX != null) disabledVFX.SetActive(false);
        ss.ShakeScreen(shakeDur, shakeMag);
        sm.PlaySound(1);
        gc.points += pointsOnKill;
        player.GetComponent<Player>().money += moneyOnKill;
        GameObject vfx = Instantiate(DeathVFX, this.transform.position, Quaternion.identity);
        Destroy(vfx, 2f);
        Destroy(this.gameObject);
    }
    
}
