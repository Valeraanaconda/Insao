using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public MobileController joystick;
    public float speed;
    public float move;
    public float jumpFoce;

    public Rigidbody2D rd;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask ground;


    private int extraJumps;
    public int extraJumpsValue;

    private void Start()
    {
        extraJumps = extraJumpsValue;
        rd = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);

        move = joystick.Horizontal();
        if (joystick.Vertical() > 0 && extraJumps > 0)
        {
            Jump();
        }
        else if (joystick.Vertical() > 0 && extraJumps == 0 && isGrounded == true)
        {
            Jump();
        }


        rd.velocity = new Vector2(move * speed, rd.velocity.y);

        if (facingRight == false && move > 0)
        {
            Flip();
        }
        else if (facingRight == true && move < 0)
        {
            Flip();
        }
    }
    public void Jump()
    {
        rd.velocity = Vector2.up * jumpFoce;
        extraJumps--;
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
