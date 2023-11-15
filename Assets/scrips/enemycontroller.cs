using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycontroller : MonoBehaviour
{
    [SerializeField]
    private float maxhealth, knockbackforce= 1f,knockbackdurition=0.5f;
    [SerializeField]
    private bool applykonkback;

    private float currenthealth, knockbackstart;
    private Vector2 rbnow,knockbackdirection;
    private bool knockback;
    private GameObject alive, brokenhead, brokenbot,body;
    private Rigidbody2D rbalive, rbbrokenhead, rbbrokenbot;
    private Animator aliveanim;


    private void Update()
    {
        checkkonkback();
    }
    private void Start()
    {
        currenthealth = maxhealth;
        alive = transform.Find("alive").gameObject;
        brokenbot = transform.Find("botton").gameObject;
        brokenhead = transform.Find("head").gameObject;
        body = transform.gameObject;
        aliveanim = alive.GetComponent<Animator>();

        rbalive = alive.GetComponent<Rigidbody2D>();
        rbbrokenbot= brokenbot.GetComponent<Rigidbody2D>();
        rbbrokenhead = brokenhead.GetComponent<Rigidbody2D>();

        
        alive.SetActive(true);
        brokenhead.SetActive(false);
        brokenbot.SetActive(false);

    }
    private void damage(float amount)
    {
        currenthealth -= amount;
      aliveanim.SetTrigger("damage");
        if (applykonkback && (currenthealth > 0.0f))
        {
            
            knockbacking();
        }
        if (currenthealth <= 0.0f)
        {
            Die();
        
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ammo"))
        {
            damage(1);
        }
    }
    private void knockbacking()
    {

        knockback = true;
        knockbackstart = Time.time;
        rbnow = Movement.playerpos;
        knockbackdirection = rbalive.position - rbnow;
        rbalive.velocity =new Vector2(knockbackdirection.x*knockbackforce,knockbackdirection.y*knockbackforce);
    
    }
    private void checkkonkback()
    {
        if (Time.time >= knockbackstart + knockbackdurition && knockback)
        {
            knockback = false;
            rbalive.velocity = new Vector2(0.0f, rbalive.velocity.y);
        }
    }
    private void Die()
    {
        brokenbot.transform.position = alive.transform.position;
        brokenhead.transform.position = alive.transform.position;

        alive.SetActive(false);
        brokenbot.SetActive(true);
        brokenhead.SetActive(true);
        rbbrokenbot.velocity = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        rbbrokenhead.velocity = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        Invoke("Destorybody", 5);
    }
    private void Destorybody()
    {
        Destroy(body);
    }
}





