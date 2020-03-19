using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    [Header("Controlles")]
    public SoundManager soundManager;
    public GameManager gameManager;
    [SerializeField] private string X;
    [SerializeField] private string Y;
    [SerializeField] private KeyCode jump;

    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    public AnimationScript anim;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
   // public float dashSpeed = 20;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
   // public bool isDashing;

    [Space]

    private bool groundTouch;
   // private bool hasDashed;

    public int side = 1;

    [Space]
    [Header("Polish")]
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameStart)
        {
            float x = 0;
            if (!coll.onWall)
                x = Input.GetAxis(X);
            float y = Input.GetAxis(Y);
            float xRaw = Input.GetAxisRaw(X);
            float yRaw = Input.GetAxisRaw(Y);
            Vector2 dir = new Vector2(x, y);

            Walk(dir);
              anim.SetHorizontalMovement(x, y, rb.velocity.y);

            if (coll.onWall && Input.GetButton("Fire3") && canMove)
            {
                if (side != coll.wallSide)
                       anim.Flip(side*-1);
                    wallGrab = true;
                wallSlide = false;
            }

            if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
            {
                wallGrab = false;
                wallSlide = false;
            }

            if (coll.onGround)
            {
                wallJumped = false;
                GetComponent<BetterJumping>().enabled = true;
            }

            if (wallGrab)
            {
                rb.gravityScale = 0;
                if (x > .2f || x < -.2f)
                    rb.velocity = new Vector2(rb.velocity.x, 0);

                float speedModifier = y > 0 ? .5f : 1;

                rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
            }
            else
            {
                rb.gravityScale = 3;
            }

            if (coll.onWall && !coll.onGround)
            {
                if (x != 0 && !wallGrab)
                {
                    wallSlide = true;
                    WallSlide();
                }
            }

            if (!coll.onWall || coll.onGround)
                wallSlide = false;

            if (Input.GetKeyDown(jump))
            {
                 anim.SetTrigger("jump");

                if (coll.onGround)
                    Jump(Vector2.up, false);
                if (coll.onWall && !coll.onGround)
                    WallJump();
            }

            if (coll.onGround && !groundTouch)
            {
                GroundTouch();
                groundTouch = true;
            }

            if (!coll.onGround && groundTouch)
            {
                groundTouch = false;
            }

            WallParticle(y);

            if (wallGrab || wallSlide || !canMove)
                return;

            if (x > 0)
            {
                side = 1;
                 anim.Flip(side);
            }
            if (x < 0)
            {
                side = -1;
                  anim.Flip(side);
            }
        }

    }

    void GroundTouch()
    {
        side = anim.sr.flipX ? -1 : 1;

       jumpParticle.Play();
    }


    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= 1;
            anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.4f + wallDir / 3.5f), true);

        wallJumped = true;
    }

    void WallSlide()
    { 
        if(coll.wallSide != side)
         anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = true;
        //if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        //{
        //    Debug.LogError("Pushing Wall");
        //    pushingWall = true;
        //}
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);

        }
        //else
        //{
        //    rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        //}
    }

    private void Jump(Vector2 dir, bool wall)
    {
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x * 1.5f, 0);
        rb.velocity += dir * jumpForce;
        soundManager.VfxPlay(SoundType.Jump);
        particle.Play();
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    void WallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (wallSlide || (wallGrab && vertical < 0))
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
           main.startColor = Color.clear;
        }
    }

    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }
}
