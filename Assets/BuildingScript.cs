using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingScript : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public Rigidbody2D rb;
    public SpriteRenderer sp;

    public float scaleCoeff;
    public float propulsion;
    public GameObject scrap;

    public Color baseColor;

    // Start is called before the first frame update
    void Start()
    {
        baseColor = sp.color;
        maxHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        float hpP = hp / maxHp;
        Color c = Color.white * hpP;
        c.a = 1;
        sp.color = c;


        if (hp <= 0)
        {
            // building explodes into scrap
            int numScrap = Random.Range(3,6);
            for (int i = 0; i < numScrap; i++)
            {
                ScrapScript s = Instantiate(scrap).GetComponent<ScrapScript>();
                s.transform.position = transform.position;

                //random velocity
                Vector2 vel = new Vector2(Random.value, Random.value);
                vel = vel.normalized * propulsion * GameManager.instance.timeSpeedFactor;

                // add velocity to scrap
                s.rb.velocity = vel;

                s.transform.localScale = Vector3.one * (scaleCoeff / (1 + 0.4f * (numScrap + 0.5f)));
                s.rb.mass = 10 / (1 + 0.5f * (numScrap + 0.5f));
            }

            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        hp -= 1;
    }
}
