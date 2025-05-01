using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float health;

    public bool live;
    public float dmgCoeff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 0.05f)
        {
            live = true;
        }
        else
        {
            live = false;
        }

        // when its velocity is low enough it turns into terrain
        if (rb.velocity.magnitude <= 0.01f)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HorseScript>() != null)
        {
            HorseScript horse = collision.gameObject.GetComponent<HorseScript>();

            health -= 1;

            if (health <= 0)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;

                Vector2 v = this.transform.position - horse.transform.position;
                v = v.normalized * horse.speed;
                rb.velocity = v;
            }


            // if live DO DAMAGE!!!!
            if (((dmgCoeff * rb.velocity.magnitude) > 5f) && live && !(Vector2.Angle(rb.velocity,horse.rb.velocity) < 90))
            {
                Debug.Log(Vector2.Angle(rb.velocity, horse.rb.velocity).ToString());
                horse.health -= dmgCoeff * rb.velocity.magnitude;
            }
        }
    }
}
