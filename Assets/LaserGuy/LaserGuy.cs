using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class LaserGuy : MonoBehaviour
{
    public HorseScript horse;
    public CrazyOrb orb;

    public float laserCharge;
    public float laserChargeMax;
    public float laserChargeP;
    public float chargeSpeed;
    public float laserSpeed;
    public int numBounces;
    public float laserDamage;
    public float orbRecoil;

    public prefabData prefabData;

    // Update is called once per frame
    public void Update()
    {
        Laser(orb.pointing);

        laserChargeP = laserCharge / laserChargeMax;
    }

    public void Laser(Vector3 dir)
    {
        // Charge Laser
        if (laserCharge <= laserChargeMax)
        {
            laserCharge += chargeSpeed * GameManager.instance.timeSpeedFactor * Time.deltaTime;
        }

        // FIRE
        if ((laserChargeMax - laserCharge) < 0.001f)
        {
            laserCharge = 0;

            LaserScript l = Instantiate(prefabData.laser).GetComponent<LaserScript>();
            l.transform.position = orb.orb.transform.position;
            l.dir = dir;
            l.speed = laserSpeed * GameManager.instance.timeSpeedFactor;
            l.numBounces = numBounces;

            bool crit = Random.value <= horse.critChance / 100;
            if (crit)
            {
                orb.rb.velocity = -1 * dir * orbRecoil * horse.critDamage;
            }
            else
            {
                orb.rb.velocity = -1 * dir * orbRecoil;
            }
            orb.stunned = 0.15f;

        }
    }
}
