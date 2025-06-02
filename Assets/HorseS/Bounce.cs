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
    public void OnCollisionEnter2D(Collision2D collision)
    {
        OnBounce(collision);
    }
    
    public void OnBounce(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HorseScript>() != null)
        {
            HorseScript h = collision.gameObject.GetComponent<HorseScript>();

            // Deal Damage when horses hit
            if (!h.invincible)
            {
                horse.damageHandler.DealDamage(horse.strength, horse.critChance, h, false, false);

                horse.audioScript.audioPlayer.PlayOneShot(horse.audioScript.bounceHit, horse.audioScript.volume);
            }
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
