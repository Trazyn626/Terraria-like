using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatcontroller : MonoBehaviour
{
    [SerializeField]
    private bool combatenabled;
    [SerializeField]
    private float inputimer, attack1ridus, attack1damage;
    [SerializeField]
    private Transform attack1hitpos;
    [SerializeField]
    private LayerMask whatisdamageable;
    private bool gotinput, isattacking;
    private float lastinputtime = Mathf.NegativeInfinity;
    private Animator anim;
    private Item item;
    public Transform weapontransform;
    public  static   bool startwave;
    private bool startrotate;
    public static Vector2 gundirection;
    private float  lastwavetime;
    public GameObject ammo;
    private bool gotopenfire;
    private bool isshooting;
    private float lastshoottime;
    private bool firstshoot;
    [SerializeField] private SpriteRenderer sr;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canattack", combatenabled);
        firstshoot = true;
    }
    private void refreashweponimage()
    {
        if (item != null)
        {
            if (startwave||startrotate)
            {
                // if (item.type == ItemType.meleeweapon || item.type == ItemType.rangeweapon || item.type == ItemType.Tool||item.type == ItemType.BuildingBlock)
                // {
                sr.sprite = item.image;
                //}
            }
            else
            {
                sr.sprite = null;
            }
        }
        else
        {
            sr.sprite = null;
        }

    }
    private void Update()
    {   
        
        item = InVentorymannger.instance.GetselectedItem(false);
       
        checkcombatinput();
        checkattack();
        refreashweponimage();
        WavingWeapon();
        rotateGun();
        checkshoot();
    }
    private void checkcombatinput()
    {
        if (BuildingSystem.canaction)
        {
            if (item != null)
            {
                if (Input.GetMouseButton(0) && (InVentorymannger.selecteditem.type == ItemType.meleeweapon || InVentorymannger.selecteditem.type == ItemType.Tool || InVentorymannger.selecteditem.type == ItemType.BuildingBlock))
                {
                    startwave = true;
                    if (item.type == ItemType.meleeweapon)
                    {

                        if (combatenabled)

                        {


                            gotinput = true;
                            lastinputtime = Time.time;

                        }
                    }

                }
                else
                {
                    startwave = false;

                }
                if (Input.GetMouseButton(0) && (InVentorymannger.selecteditem.type == ItemType.rangeweapon))
                {
                    startrotate = true;

                    gotopenfire = true;


                }
                else
                {
                    gotopenfire = false;
                    startrotate = false;
                    if (Time.time - lastshoottime > item.usingSpeed)
                    {

                        firstshoot = true;
                    }
                }
            }
            else
            {
                startwave = false;
                startrotate = false;

            }
        }
        else
        {
            startwave = false;
            startrotate = false;
            gotopenfire = false;

        }
    }

    private void checkattack()
    {

        if (gotinput)
        {
            if (!isattacking)
            {
                gotinput = false;
                isattacking = true;
                anim.SetBool("attackone", true);
                WavingWeapon();
                anim.SetBool("isattack", isattacking);
            }

        }
        if (Time.time >= lastinputtime + inputimer)

        {
            gotinput = false;
        }
    }
    private void checkshoot()
    {

        if (gotopenfire)
        {
            isshooting = true;
            if (lastshoottime == 0)
            {
                lastshoottime = Time.time;
            }
            if (Time.time - lastshoottime > item.usingSpeed)
            { //  anim.SetBool("attackone", true);
                GameObject ammo1 = Instantiate(ammo, attack1hitpos.position, Quaternion.identity);
                // anim.SetBool("isattack", isattacking);
                lastshoottime = 0;
            }
            if (firstshoot)
            {
                GameObject ammo2 = Instantiate(ammo, attack1hitpos.position, Quaternion.identity);
                lastshoottime = 0;
                firstshoot = false;
            }
        }
    }
    private void checkattackhitbox()
    {
        Collider2D[] detectedobjects = Physics2D.OverlapCircleAll(attack1hitpos.position, attack1ridus, whatisdamageable);
        foreach (Collider2D collider in detectedobjects)
        {
            collider.transform.parent.SendMessage("damage", attack1damage);

        }
    }
    private void finishattack()
    {
        isattacking = false;
        anim.SetBool("isattack", isattacking);
        anim.SetBool("attackone", false);
        startwave = false;
   
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1hitpos.position, attack1ridus);
    }
    private void rotateGun()
    {


        if (startrotate)
        {
            gundirection = (BuildingSystem.mousePos - weapontransform.position).normalized;
            float angel = Mathf.Atan2(gundirection.y, gundirection.x) * Mathf.Rad2Deg;
            weapontransform.eulerAngles = new Vector3(0, 0, angel-135);
        
        }
    }
    private void WavingWeapon()
    {
        if (startwave) 
        {
         
            
                if (lastwavetime == 0)
                {
                    lastwavetime = Time.time;
                }

                 weapontransform.Rotate(0, 0,-180/item.usingSpeed*Time.deltaTime);
                if (Time.time - lastwavetime > item.usingSpeed)
                {
                    Resetweaponrotate();
                    lastwavetime = 0;
                }

            

           
           
           
        }
       
    }

    private void Resetweaponrotate()
    {
        weapontransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        
    }
 }
