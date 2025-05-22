using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Movement : MonoBehaviour
{
    public HorseScript horse;

    public Vector2 facing;


    // Start is called before the first frame update
    void Start()
    {
        horse.rb.velocity = facing.normalized * horse.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (horse.stunned <= 0)
        {
            facing = horse.rb.velocity;
            if (facing.x < 0)
            {
                horse.spriteHandler.sprite.flipX = true;
            }
            else
            {
                horse.spriteHandler.sprite.flipX = false;
            }

            // correct velocity to always equal speed
            Vector2 vec = horse.rb.velocity;
            horse.rb.velocity = vec.normalized * horse.speed;
        }
    }
}
