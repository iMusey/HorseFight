using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class HorseScript : MonoBehaviour
{
    public string horseName;
    // stats
    public float speed;
    public float maxHealth;
    public float health;
    public float hpP;
    public float armor;
    public float strength;
    public float critChance;
    public float critDamage;
    public float cooldownReduction;

    public bool invincible;
    public float maxIFrames;
    public float iFrames;
    public float stunned;


    // object references
    public Rigidbody2D rb;
    public Movement movement;
    public Bounce bounce;
    public StatManager statManager;
    public SpriteHandler spriteHandler;
    public AudioScript audioScript;
    public DamageHandler damageHandler;


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

        // iFrame manager
        if (iFrames > 0)
        {
            iFrames -= Time.deltaTime;
        }
        else
        {
            invincible = false;
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

    public void TakeDamage(float dmg)
    {
        // Actually deal the damage
        health -= dmg - armor;
        invincible = true;
        iFrames = maxIFrames;
    }


    public void Explosion(float dmg, Color c)
    {
        // instantiate explosion
        ExplosionScript exp = Instantiate(prefabData.explosion).GetComponent<ExplosionScript>();
        exp.transform.SetParent(transform, false);

        // size of explosion depending on damage
        float explosionScale = 1 + dmg / maxHealth;
        Vector3 newScale = exp.transform.localScale;
        newScale *= explosionScale;
        exp.transform.localScale = newScale;
        
        // set position and color of explosion
        Vector3 pos = transform.position;
        pos.x += Random.Range(-1, 1f) / (6f - explosionScale);
        pos.y += Random.Range(-1, 1f) / (6f - explosionScale);
        exp.transform.position = pos;

        /*
        Vector3 expColor = new Vector3(c.r + prefabData.colors[5].r, c.g + prefabData.colors[5].g, c.b + prefabData.colors[5].b);
        expColor /= 2f;
        c = new Color(expColor.x, expColor.y, expColor.z);*/
        //c = c * prefabData.colors[5];
        exp.color = c;
    }
}
