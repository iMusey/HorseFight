using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float strength;

    public bool live;
    public float maxCD;
    public float cooldown;

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
            float opacity = 1 - cooldown / maxCD;

            if (cooldown < 0)
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
            live = false;
            cooldown = maxCD;
            horse.health += strength;
        }
    }
}
