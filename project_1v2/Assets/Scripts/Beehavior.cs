using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beehavior : MonoBehaviour
{
    private float speed;
    public int damage = 1;
    public Transform target;

    void Start()
    {
        speed = 3.0f;
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        // Bees chase the player.
        transform.Translate(0, 0, speed * Time.deltaTime);
        transform.LookAt(target);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();

        // Bees hurt player upon touching them and die.
        if (player != null)
        {
            player.Hurt(damage);
        }
        Destroy(this.gameObject);
    }
}
