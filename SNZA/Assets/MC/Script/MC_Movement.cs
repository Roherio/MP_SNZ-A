using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Movement : MonoBehaviour
{
    private float horizontal;
    private float speed;
    private float jumpPower;
    private bool isFacingRight = true;
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        //si pulses jump i toques terra saltes
        if(Input.GetButtonDown("Jump") && IsGround())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
        //si pulses jump I ESTAS ANANT CAP AMUNT, prolongaràs una mica el salt
        if(Input.GetButtonDown("Jump") && body.velocity.y > 0f)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
        }
        Flip();

    }
    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * speed, body.velocity.y);
    }
    private bool IsGround()
    {
        //crea cercle al voltant del player, si fa overlap amb el ground, permet saltar
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}