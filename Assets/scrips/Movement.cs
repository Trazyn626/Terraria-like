using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Rigidbody2D rb;
    public static float xspeed;
    public static Vector2 playerpos;
    private Animator anim;
    private float Direction;
    public float moventspeed = 3;
    public float groundCheckradius = 0.13f;
    private bool isfacingright = true;
    public bool isground;
    private bool Canjump, canflip;
    public Transform raycastCenter;
    public float jumpforce = 4;
    public Transform groundCheck;
    public LayerMask theground;
    private bool iswalking;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        
        checkifcanjump();
        Checkinput();
        Checkmovementdirection();
        
        autojump();
        xspeed = rb.velocity.x;
        playerpos = rb.position;
        
    }
    private void checkifcanjump()
    {
        
       if (isground==false)// &&Mathf.Abs( rb.velocity.y )>= 0.01f)
        {
            Canjump = false;
        }
        if (isground==true)//&&Mathf.Abs(  rb.velocity.y )<= 0.01f)
        {
            Canjump = true;
        }
    }

    private void FixedUpdate()
    {
        Applymovement();
        checksurround();
        updatanimations();
    }

    private void updatanimations()
    {
        anim.SetBool("iswalking", iswalking);


    }

    private void checksurround()
    {

        isground = Physics2D.OverlapCircle(groundCheck.position, groundCheckradius, theground);

    }
    private void Checkmovementdirection()
    {
        if (isfacingright && Direction < 0)
        {
            Flip();
        }
        else if (!isfacingright && Direction > 0)
        {
            Flip();

        }
        if ((Mathf.Abs(rb.velocity.x) > 0.05) && (Input.GetAxisRaw("Horizontal") != 0))
        {
            iswalking = true;
        }
        else { iswalking = false; }
    }
    private void Checkinput()
    {
        Direction = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
            Canjump = false;
        }
    }


    private void Applymovement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (isground)
            {
                rb.velocity = new Vector2(moventspeed * Direction, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0.8f * moventspeed * Direction, rb.velocity.y);
            }
        }
        else
        {
            if (isground)
            {
                rb.velocity = Vector2.Lerp(new Vector2(rb.velocity.x, rb.velocity.y), new Vector2(0, rb.velocity.y), 0.4f);
            }
            else
            {
                rb.velocity = Vector2.Lerp(new Vector2(rb.velocity.x, rb.velocity.y), new Vector2(0, rb.velocity.y), 0.02f);
            }


        }


    }

    private void autojump()

    {
        if(Input.GetAxisRaw("Horizontal") != 0)
        { 
        RaycastHit2D hit1 = Physics2D.Raycast(raycastCenter.position, transform.right, 1f, theground); 
        RaycastHit2D hit2 = Physics2D.Raycast(raycastCenter.position+Vector3.up*1.1f, transform.right, 1f, theground);
    
        if (hit2 == false && hit1 == true)
        {
           
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up, 20*Time.deltaTime);
        }

        }

    }
    private void Flip()
    {
    
        
            isfacingright = !isfacingright;
            transform.Rotate(0.0f, 180f, 0.0f);
       
    }


    private void Jump()
    {
        if (Canjump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckradius);

    }

}
