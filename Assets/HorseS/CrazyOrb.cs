using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyOrb : MonoBehaviour
{
    public HorseScript horse;
    public LaserGuy lGuy;

    public GameObject orb;
    public Rigidbody2D rb;
    public float rotationalSpeedFactor;
    public float gravitationalPull;
    public float gravitationalSpeed;
    public float stunned;

    public Vector3 position;
    public Vector3 pointing;

    // Start is called before the first frame update
    void Start()
    {
        position *= horse.transform.lossyScale.x;
    }

    // Update is called once per frame
    void Update()
    {


        if (stunned <= 0)
        {
            // velocity goes in target direction
            rb.velocity = gravitationalSpeed * GameManager.instance.timeSpeedFactor * (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0)).normalized;

            // move towards center some amount
            orb.transform.position = Vector3.MoveTowards(orb.transform.position, (position + horse.transform.position), gravitationalPull * Time.deltaTime);
        }
        else
        {
            stunned -= Time.deltaTime;
        }
        

        // adjust angular velocity if not stunned or close to shooting
        if (lGuy.laserChargeP >= 0.8f || stunned > 0)
        {
            rb.angularVelocity = 0;
        }
        else
        {
            rb.angularVelocity = horse.speed * rotationalSpeedFactor * 360;
        }

        // determine which direction the orb is pointing.
        pointing = (Quaternion.Euler(orb.transform.eulerAngles) * new Vector3(1,1,0)).normalized;
    }
}
