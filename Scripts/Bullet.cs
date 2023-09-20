using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("Stats")]
    public int damage;
    public bool explodes;
    [SerializeField] private float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDir.z = 0f;
        transform.LookAt(lookDir);
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * bulletSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!explodes || collision.CompareTag("Player")) return;
        Destroy(this.gameObject);
    }
}
