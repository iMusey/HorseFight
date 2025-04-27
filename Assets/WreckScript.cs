using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vec = transform.position;
        vec.y += 0.35f;
        text.gameObject.transform.position = Camera.main.WorldToScreenPoint(transform.position);

        countDown -= Time.deltaTime;
        text.text = (((int)countDown) + 1).ToString();

        Color color = blastRadius.color;
        color.a = .5f - countDown / 5;
        blastRadius.color = color;

        if (countDown <= 0)
        {
            Explode(transform.position);
        }
    }

    public void Explode(Vector2 center)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(center, radius);

        // EXPLODE
        foreach (Collider2D target in targets)
        {
            if (target.gameObject.GetComponent<HorseScript>() != null)
            {
                Vector2 blastDir = target.transform.position - transform.position;
                target.attachedRigidbody.velocity = blastDir.normalized * propulsion;
                HorseScript h = target.GetComponent<HorseScript>();

                h.facing = blastDir;
                h.health -= strength;

            }
        }

        // DIE
        Destroy(gameObject);
    }
}
