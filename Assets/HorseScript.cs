using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.Rendering;

public class HorseScript : MonoBehaviour
{
    // stats
    public float speed;
    public float maxHealth;
    public float health;
    public float hpP;
    public float strength;
    public float critChance;
    public float cooldownReduction;

    public bool invincible;
    public float iFrames;
    public float stunned;


    // object references
    public Rigidbody2D rb;
    public Movement movement;
    public Bounce bounce;
    public StatManager statManager;
    public SpriteHandler spriteHandler;
    public AudioScript audioScript;


    public prefabData prefabData;

    // Start is called before the first frame update
    public virtual void Start()
    {

        statManager.baseSpeed = speed;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Health percent
        hpP = health / maxHealth;


        // Speed gets faster with time
        speed = statManager.baseSpeed * GameManager.instance.timeSpeedFactor;

        /*
        Vector3 pos = rb.position;
        pos.z = 0;
        transform.position = pos; */

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        // Stun timer
        if (stunned >= 0)
        {
            stunned -= Time.deltaTime;
        }



        // Hp = 0 -> dies
        if (health <= 0)
        {
            Sprite sp = spriteHandler.sprite.sprite;
            Vector2 vel = movement.facing.normalized * speed;

            // DEATH
            Destroy(gameObject);
            GameManager.instance.numPlayers--;

            // Summon Wreck
            WreckScript w = Instantiate(prefabData.wreck).GetComponent<WreckScript>();
            w.transform.position = transform.position;
            w.sprite.sprite = sp;
            w.rb.velocity = vel;

        }
    }


    public virtual void IFrames()
    {
        iFrames -= Time.deltaTime;

        if (iFrames <= 0)
        {
            invincible = false;
        }
    }
}
