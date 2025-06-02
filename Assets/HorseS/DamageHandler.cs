using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DamageHandler : MonoBehaviour
{
    public HorseScript horse;


    public prefabData prefabData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float DealDamage(float str, float critChance, HorseScript target, bool ability, bool heal)
    {
        // randomly crit
        bool crit = Random.value <= critChance / 100;
        bool crit2 = Random.value <= critChance / 100;
        float dmg = str;
        if (crit && crit2)
        {
            dmg *= horse.critDamage * 1.5f;
        }
        else if (crit)
        {
            dmg *= horse.critDamage;
        }

        target.TakeDamage(dmg);

        if (!heal)
        {
            // reduce by armor
            dmg -= target.armor;
        }

        // spawn dmg Number
        DmgNumber(dmg, crit, ability, heal, target);

        return dmg;
    }

    public void DmgNumber(float dmg, bool crit, bool ability, bool heal, HorseScript target)
    {
        Vector3 pos = target.gameObject.transform.position;

        // spawn dmg number
        DamageNumber dmgNum = Instantiate(prefabData.dmgNum).GetComponent<DamageNumber>();

        // spawn in correct position
        pos.x += Random.Range(-1, 1) / 10;
        pos.y += Random.Range(-1, 1) / 10;
        dmgNum.text.transform.position = Camera.main.WorldToScreenPoint(pos);


        // assign correct color
        Color c = Color.white;
        if (ability)
        {
            if (crit)
            {
                c = prefabData.colors[4];
            }
            else
            {
                c = prefabData.colors[3];
            }
        }
        else if (crit)
        {
            c = prefabData.colors[1];
        }
        else if (heal)
        {
            c = prefabData.colors[2];
        }
        else
        {
            c = prefabData.colors[0];
        }


        // if damage higher than 0 make an explosion effect
        if (dmg >= 0)
        {
            target.Explosion(dmg, c);
        }

        dmgNum.text.color = c;

        if (heal)
        {
            dmgNum.text.text = Mathf.RoundToInt(-dmg).ToString();
        }
        else
        {
            dmgNum.text.text = Mathf.RoundToInt(dmg).ToString();
        }
    }
}
