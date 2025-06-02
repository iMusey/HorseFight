using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ScrapScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector3 facing;
    public float health;

    public bool live;
    public float dmgCoeff;

    public DamageHandler damageHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        facing = rb.velocity;

        if (rb.velocity.magnitude > 0.05f)
        {
            live = true;
        }
        else
        {
            live = false;
        }

        // when its velocity is low enough it turns into terrain
        if (rb.velocity.magnitude <= 0.05f)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HorseScript>() != null)
        {
            HorseScript horse = collision.gameObject.GetComponent<HorseScript>();

            health -= 1;

            // When hp reaches 0 become dynamic
            if (health <= 0)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;

                Vector3 v = this.transform.position - horse.transform.position;
                v = v.normalized * horse.speed;
                rb.velocity = v;

                health = 5;
            }

            // normalize vector of horse position relative to scrap position
            Vector3 rVec = (horse.transform.position - transform.position).normalized;

            // get dot product of relative vector and unit vector of scrap
            float dot = Vector3.Dot(facing, rVec);
            //Debug.Log(dot.ToString());

            // check if the angle is correct then do dmg
            float dmg = dmgCoeff * rb.velocity.magnitude;

            if (live && (dmg > 5f) && (dot > 0.75f))
            {
                Debug.Log(dmg);
                damageHandler.DealDamage(dmg, -1, horse, false, false);
            }
        }
    }
}
