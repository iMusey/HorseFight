using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class WreckScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public TextMeshProUGUI text;
    public SpriteRenderer sprite;
    public SpriteRenderer blastRadius;
    public float countDown;

    public float strength;
    public float propulsion;
    public float radius;

    public DamageHandler damageHandler;

    public prefabData prefabData;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 vec = transform.position;
        vec.y += 0.25f;
        text.gameObject.transform.position = Camera.main.WorldToScreenPoint(vec);
        text.text = countDown.ToString();
        text.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // text moves with wreck
        Vector2 vec = transform.position;
        vec.y += 0.35f;
        text.gameObject.transform.position = Camera.main.WorldToScreenPoint(transform.position);

        // Countdown
        countDown -= Time.deltaTime;
        text.text = (((int)countDown) + 1).ToString();


        // blast radius changes color
        Color color = blastRadius.color;
        color.a = .5f - countDown / 5;
        blastRadius.color = color;

        // Spawn Scrap
        if (countDown < 0)
        {
            int numScrap = 1;
            for (int i = 0; i < numScrap; i++)
            {
                ScrapScript s = Instantiate(prefabData.scrap).GetComponent<ScrapScript>();
                s.transform.position = transform.position;

                //random velocity
                Vector2 vel = new Vector2(UnityEngine.Random.value, UnityEngine.Random.value);
                vel = vel.normalized * propulsion * GameManager.instance.timeSpeedFactor;

                // add velocity to scrap
                s.rb.velocity = vel;

                s.transform.localScale = transform.localScale / (1 + 0.1f * (numScrap + 0.5f));
                s.rb.mass = s.rb.mass / (1 + 0.2f * (numScrap + 0.5f));
            }
            Explode(transform.position);
        }
    }

    public void Explode(Vector2 center)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(center, radius);

        // EXPLODE push all targets away
        foreach (Collider2D target in targets)
        {
            if (target.gameObject.GetComponent<HorseScript>() != null)
            {
                Vector2 blastDir = target.transform.position - transform.position;
                target.attachedRigidbody.velocity = blastDir.normalized * propulsion * GameManager.instance.timeSpeedFactor;
                HorseScript h = target.GetComponent<HorseScript>();

                h.movement.facing = blastDir;
                damageHandler.DealDamage(strength, -1, h, false, false);
                h.stunned += 1.25f;
            }
            else if (target.gameObject.GetComponent<ScrapScript>() != null)
            {
                ScrapScript s = target.GetComponent<ScrapScript>();

                Vector2 blastDir = target.transform.position - transform.position;
                target.attachedRigidbody.velocity = blastDir.normalized * propulsion * GameManager.instance.timeSpeedFactor;
                s.live = true;
                s.rb.bodyType = RigidbodyType2D.Dynamic;
                s.rb.velocity = blastDir.normalized * propulsion;
            }
        }

        // DIE
        Destroy(gameObject);
    }
}
