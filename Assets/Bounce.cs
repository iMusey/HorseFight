using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.Rendering;

public class Bounce : MonoBehaviour
{
    public HorseScript horse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {

        // Deal Damage when horses hit
        if (!horse.invincible && collision.gameObject.GetComponent<HorseScript>() != null)
        {
            HorseScript horse = collision.gameObject.GetComponent<HorseScript>();

            horse.health -= horse.strength;

            horse.audioScript.audioPlayer.PlayOneShot(horse.audioScript.bounceHit, horse.audioScript.volume);

            horse.invincible = true;
            horse.iFrames = 0.2f;
        }
        else
        {
            horse.audioScript.audioPlayer.PlayOneShot(horse.audioScript.bounce, horse.audioScript.volume);

            // Random Bounce direction sometimes
            int rand = Random.Range(0, 2);

            if (rand == 0)
            {
                Vector2 v = new Vector2((Random.Range(0, 2) * 2 - 1) * Random.Range(-1f, 1f), (Random.Range(0, 2) * 2 - 1) * Random.Range(-1f, 1f));
                horse.rb.velocity = v;
            }
        }
    }
}
