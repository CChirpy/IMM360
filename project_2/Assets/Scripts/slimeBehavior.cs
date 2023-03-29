using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
    Beehavior.cs from Project 1

    MoveTowards
    https://docs.unity3d.com/ScriptReference/Vector2.MoveTowards.html

    How to determine if an Enemy is moving left or right?
    http://answers.unity.com/answers/1859202/view.html

*/

public class slimeBehavior : MonoBehaviour
{
    public int health = 3;
    public int damage = 1;
    public float speed = 1.0f;
    public Transform target;

    private Rigidbody2D body;
    private Collider2D col;
    private Animator anim;
    private SpriteRenderer rend;

    private float oldPosition;
    private Vector2 startPosition;
    private int startHealth;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();

        target = GameObject.Find("Player").transform;
        oldPosition = transform.position.y;
        startPosition = this.gameObject.transform.position;
        startHealth = health;
    }

    void Update()
    {
        // Slime chases player
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Flip sprite depending on direction
        if (transform.position.x > oldPosition)
        { rend.flipX = false; }
        if (transform.position.x < oldPosition)
        { rend.flipX = true; }

        // Revive when health reaches 0
        if (health <= 0)
        {
            // Destroy(this.gameObject);
            transform.position = new Vector2(startPosition.x, startPosition.y);
            health = startHealth;

            // Add points to score
            menuControl.points += damage;
        }
    }

    void LateUpdate()
    {
        oldPosition = transform.position.x;
    }
}
