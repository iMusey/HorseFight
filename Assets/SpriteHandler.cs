using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHandler : MonoBehaviour
{
    public HorseScript horse;

    public SpriteRenderer sprite;
    public SpriteRenderer aura;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // aura becomes more visible when lower hp
        Color temp = aura.color;
        if (horse.hpP < 0.5f)
        {
            temp.a = 0.5f - horse.hpP;
        }
        else
        {
            temp.a = 0;
        }
        aura.color = temp;
        aura.gameObject.transform.localScale = Vector3.one * (1 + (0.5f - horse.hpP));

        Color c = sprite.color;
        if (horse.invincible)
        {
            horse.IFrames();
            c.a = 0.5f;
        }
        else
        {
            c.a = 1f;
        }
        sprite.color = c;
    }
}
