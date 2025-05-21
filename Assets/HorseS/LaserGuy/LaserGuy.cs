using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class LaserGuy : HorseScript
{
    public float laserCharge;
    public float laserChargeMax;
    public GameObject LaserObj;
    public float chargeSpeed;
    public float laserSpeed;
    public int numBounces;

    public float laserDamage;
    public float laserAngle;

    public SpriteRenderer laserPointer;

    // Update is called once per frame
    public override void Update()
    {
        laserAngle = Random.Range(1, 360);
        laserAngle *= Mathf.Deg2Rad;

        Laser();
        



        // speed gets faster with time
        speed = baseSpeed * GameManager.instance.timeSpeedFactor;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (stunned >= 0)
        {
            stunned -= Time.deltaTime;
        }

        if (stunned <= 0)
        {
            facing = rb.velocity;
            if (facing.x < 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }

            // correct velocity to always equal speed
            Vector2 vec = rb.velocity;
            rb.velocity = vec.normalized * speed;
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


        // Hp = 0 -> dies
        if (health <= 0)
        {
            Sprite sp = sprite.sprite;
            Vector2 vel = facing.normalized * speed;

            // DEATH
            Destroy(gameObject);
            GameManager.instance.numPlayers--;

            // Summon Wreck
            WreckScript w = Instantiate(wreck).GetComponent<WreckScript>();
            w.transform.position = transform.position;
            w.sprite.sprite = sp;
            w.rb.velocity = vel;

        }
    }

    public void Laser()
    {
        // Charge Laser
        if (laserCharge <= laserChargeMax)
        {
            laserCharge += chargeSpeed * GameManager.instance.timeSpeedFactor * Time.deltaTime;
        }

        // FIRE
        if ((laserChargeMax - laserCharge) < 0.1f)
        {
            laserCharge = 0;

            Vector2 dir = new Vector2(Mathf.Cos(laserAngle), Mathf.Sin(laserAngle));

            LaserScript l = Instantiate(LaserObj).GetComponent<LaserScript>();
            l.transform.position = laserPointer.transform.position;
            l.dir = dir;
            l.speed = laserSpeed * GameManager.instance.timeSpeedFactor;
            l.numBounces = numBounces;
        }
    }

    public void FireLaser()
    {

    }
}
