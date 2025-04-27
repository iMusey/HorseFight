using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class HorseScript : MonoBehaviour
{
    public float speed;
    private float baseSpeed;
    public float maxHealth;
    public float health;
    public float strength;

    public float hpP;

    public bool invincible;
    public float iFrames;

    public Vector2 facing;

    public SpriteRenderer sprite;
    public SpriteRenderer aura;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = facing.normalized * speed;

        baseSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        // speed manipulation

        // speed gets faster with time
        speed = baseSpeed * GameManager.instance.timeSpeedFactor;

        // correct velocity to always equal speed
        Vector2 vec = rb.velocity;
        rb.velocity = vec.normalized * speed;

        // 
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        // Hp = 0 -> dies
        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.numPlayers--;
        }


        facing = rb.velocity;
        if (facing.x < 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        // Health
        hpP = health / maxHealth;

        // aura becomes more visible when lower hp
        Color temp = aura.color;
        if (hpP < 0.5f)
        {
            temp.a = 0.5f - hpP;
        }
        else
        {
            temp.a = 0;
        }
        aura.color = temp;
        aura.gameObject.transform.localScale = Vector3.one * (1 + (0.5f - hpP));


        Color c = sprite.color;
        if (invincible)
        {
            IFrames();
            c.a = 0.5f;
        }
        else
        {
            c.a = 1f;
        }
        sprite.color = c;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Deal Damage when horses hit
        if (!invincible && collision.gameObject.GetComponent<HorseScript>() != null)
        {
            HorseScript horse = collision.gameObject.GetComponent<HorseScript>();

            horse.health -= strength;

            invincible = true;
            iFrames = 0.2f;
        }
        else
        {
            // Random Bounce direction sometimes
            int rand = Random.Range(0, 2);

            if (rand == 0)
            {
                Vector2 v = new Vector2((Random.Range(0, 2) * 2 - 1) * Random.Range(-1f, 1f), (Random.Range(0, 2)*2-1)*Random.Range(-1f,1f));
                rb.velocity = v;
            }
        }
    }

    public void IFrames()
    {
        iFrames -= Time.deltaTime;

        if (iFrames <= 0)
        {
            invincible = false;
        }
    }
}
