using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float strength;

    public bool live;
    public float maxCD;
    public float cooldown;

    public DamageHandler damageHandler;
    public SpriteRenderer sprite;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!live)
        {
            cooldown -= Time.deltaTime;
            float opacity = 0;

            if ((GameManager.instance.numPlayers > 2) && cooldown < 0)
            {
                live = true;
                opacity = 1;
            }

            Color newColor = sprite.color;
            newColor.a = opacity;
            sprite.color = newColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (live && col.gameObject.GetComponent<HorseScript>() != null)
        {
            HorseScript horse = col.gameObject.GetComponent<HorseScript>();
            if (horse.hpP == 1)
            {
                return;
            }
            live = false;
            cooldown = maxCD * GameManager.instance.timeSpeedFactor;
            damageHandler.DealDamage(strength, 0, horse, false, true);
        }
    }
}
