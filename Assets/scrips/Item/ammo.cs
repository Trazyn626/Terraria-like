using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo : MonoBehaviour
{
    private Rigidbody2D rb;
    public float flyingspeed;
    private Vector2 dir;
    private float time;
    void Awake()
    {
        dir = combatcontroller.gundirection.normalized;
        rb = GetComponent<Rigidbody2D>();
        time = Time.time;

    }
    private void Update()
    {

        rb.velocity = dir* flyingspeed;
        if (Time.time - time > 5)
        {
            Destroy(gameObject);      
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ground")|| other.CompareTag("enemy"))
        {
          Destroy(gameObject);
        }
    }
   
}