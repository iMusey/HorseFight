using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckinBronco : HorseScript
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {

        // Deal Damage when horses hit
        if (!invincible && collision.gameObject.GetComponent<HorseScript>() != null)
        {
            HorseScript horse = collision.gameObject.GetComponent<HorseScript>();

            horse.health -= strength;

            audioPlayer.PlayOneShot(bounceHit, volume);

            // Random Bounce direction sometimes
            float rand = Random.value;

            if (rand < 0.95)
            {
                Vector2 v = new Vector2((Random.Range(0, 2) * 2 - 1) * Random.Range(-1f, 1f), (Random.Range(0, 2) * 2 - 1) * Random.Range(-1f, 1f));
                rb.velocity = v;
            }
            else
            {
                baseSpeed += 0.5f;
            }

            invincible = true;
            iFrames = 0.2f;
        }
        else
        {
            audioPlayer.PlayOneShot(bounce, volume);

            // Random Bounce direction sometimes
            float rand = Random.value;

            if (rand < 0.95)
            {
                Vector2 v = new Vector2((Random.Range(0, 2) * 2 - 1) * Random.Range(-1f, 1f), (Random.Range(0, 2) * 2 - 1) * Random.Range(-1f, 1f));
                rb.velocity = v;
            }
            else
            {
                baseSpeed += 0.1f;
            }
        }
    }
}
