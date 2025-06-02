using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Movement : MonoBehaviour
{
    public HorseScript horse;

    public Vector3 facing;


    // Start is called before the first frame update
    void Start()
    {
        horse.rb.velocity = facing.normalized * horse.speed;
    }

    // Update is called once per frame
    void Update()
    {
        facing = horse.rb.velocity.normalized;

        if (horse.stunned <= 0)
        {
            if (facing.x < 0)
            {
                horse.spriteHandler.sprite.flipX = true;
            }
            else
            {
                horse.spriteHandler.sprite.flipX = false;
            }

            // correct velocity to always equal speed
            horse.rb.velocity = facing * horse.speed;
        }
    }
}
