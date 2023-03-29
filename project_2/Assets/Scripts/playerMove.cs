using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
    Movement Of a 2D Player in Unity: 
    https://medium.com/@chamo.wijetunga/movement-of-a-2d-player-in-unity-aff2b8f02fb5

    How to jump in Unity using physics:
    https://gamedevbeginner.com/how-to-jump-in-unity-with-or-without-physics/#jump_unity

    how do i check if my rigidbody player is grounded?
    http://answers.unity.com/answers/196395/view.html

    ch06: PlatformerPlayer.cs

    My Player can 'Climb' walls, how do I stop this?
    http://answers.unity.com/answers/1804876/view.html
    https://docs.unity3d.com/ScriptReference/Physics2D.OverlapArea.html

    Introduction to Unity’s Animator:
    https://medium.com/geekculture/introduction-to-unitys-animator-baac57eccc8a
    https://docs.unity3d.com/Manual/AnimationParameters.html

    https://docs.unity3d.com/ScriptReference/Collider2D.OnCollisionStay2D.html
*/

public class playerMove : MonoBehaviour
{
    public static float deltaY;
    public static float deltaX;

    public int health = 3;
    public Vector2 speed = new Vector2(2, 6);

    private float inputX;
    private Vector2 moveX;
    private float startY;
    private float startX;

    private int atkState;
    private float timePassed;
    private bool isAttacking;
    private bool isAlive;

    private Rigidbody2D body;
    private Collider2D col;
    private Animator anim;
    private SpriteRenderer rend;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();

        startY = transform.position.y;
        startX = transform.position.x;
        // Debug.Log(startY);

        atkState = 0;
        timePassed = 0;
        isAlive = true;
    }

    void Update()
    {
        // Debug.Log(IsGrounded());

        // Horizontal Movement
        if (isAlive)
        {
            inputX = Input.GetAxis("Horizontal");
            moveX = new Vector2(speed.x * inputX, 0) * Time.deltaTime;
            transform.Translate(moveX);
        }

        // Run Animation
        if (inputX != 0 && isAlive)
        {
            anim.SetBool("Run", true);

            if (Input.GetKey(KeyCode.A) == true)
            { rend.flipX = true; }
            if (Input.GetKey(KeyCode.D) == true)
            { rend.flipX = false; }
        }
        else
        {
            anim.SetBool("Run", false);
        }

        // Jump, Vertical Movement
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && isAlive)
        {
            body.AddForce(Vector2.up * speed.y, ForceMode2D.Impulse);
        }
        anim.SetBool("Grounded", IsGrounded());

        // Keep track of deltaY and deltaX
        deltaY = transform.position.y - startY;
        deltaX = transform.position.x - startX;

        // Attack
        if (Input.GetKeyDown(KeyCode.Mouse0) && isAlive)
        {
            isAttacking = true;

            if (atkState > 2)
            {
                atkState = 0;
            }
            atkState += 1;
            anim.SetInteger("Attack", atkState);

            // Slow down player while attacking
            speed /= 1.5f;
            timePassed = 0;
        }
        else
        {
            anim.SetInteger("Attack", 0);
        }

        // Regain speed after not attacking
        timePassed += Time.deltaTime;
        if (timePassed >= .4f)
        {
            isAttacking = false;
            speed = new Vector2(2, 6);
        }

        // Death
        if (health <= 0)
        {
            isAlive = false;
            anim.SetBool("isDead", true);
        }
    }

    // Ground Check, returns boolean
    public bool IsGrounded()
    {
        Vector2 corner1 = new Vector2(col.bounds.max.x, col.bounds.min.y - .1f);
        Vector2 corner2 = new Vector2(col.bounds.min.x, col.bounds.min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2, LayerMask.GetMask("Floor"));

        if (hit != null)
        { return true; }
        else { return false; }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        // Deal damage to slimes
        slimeBehavior slime = other.gameObject.GetComponent<slimeBehavior>();
        Rigidbody2D slimebody = other.gameObject.GetComponent<Rigidbody2D>();

        if (isAttacking == true && other.gameObject.tag == "Enemy")
        {
            // Deal damage
            slime.health -= 1;

            // Knockback
            Vector2 direction = new Vector2(1f, 0.5f);
            if (rend.flipX == false)
            { direction = new Vector2(1f, direction.y); }
            else if (rend.flipX == true)
            { direction = direction = new Vector2(-1f, direction.y); }
            slimebody.AddForce(direction * 100f);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            // Take damage
            health -= 1;
        }

        // Debug.Log("Collider entered trigger with " + other.gameObject.name);
    }
}
