using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class HorseScript : MonoBehaviour
{
    public float speed;
    private float baseSpeed;
    public float maxHealth;
    public float health;
    public float strength;

    public Vector2 facing;

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


        if (health > maxHealth)
        {
            health = maxHealth;
        }

        // Hp = 0 -> dies
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Deal Damage
        if (collision.gameObject.GetComponent<HorseScript>() != null)
        {
            HorseScript horse = collision.gameObject.GetComponent<HorseScript>();

            horse.health -= strength;
        }
        else
        {
            // Bounce direction
            int rand = Random.Range(0, 2);

            if (rand == 0)
            {
                Debug.Log("merp");
                Vector2 v = new Vector2((Random.Range(0, 2) * 2 - 1) * Random.Range(-1f, 1f), (Random.Range(0, 2)*2-1)*Random.Range(-1f,1f));
                Debug.Log(v);
                rb.velocity = v;
            }
            else
            {
                // bounce away 
            }
        }
    }
}
