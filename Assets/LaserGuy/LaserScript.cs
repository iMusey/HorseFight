using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public LaserTracker[] points;
    public int numBounces;
    public float duration;
    public float speed;
    public float damage;
    public bool crit;
    public Vector3 dir;

    public prefabData prefabData;


    void Start()
    {
        // set up first tracker
        points = new LaserTracker[numBounces + 1];

        LaserTracker t = Instantiate(prefabData.laserTracker).GetComponent<LaserTracker>();
        t.start = t.transform.position;
        t.transform.position = transform.position;
        t.laser = this;
        t.rb.velocity = dir.normalized * speed;
        t.transform.parent = this.transform;

        points[0] = t;
    }

    // Update is called once per frame
    void Update()
    {
        // duration of beam determines when it
        if (numBounces > 0)
        {
            duration -= Time.deltaTime/2;
        }
        else
        {
            duration -= Time.deltaTime*2;
        }

        if (duration <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Bounce()
    {
        if (numBounces > 0)
        {
            LaserTracker t = Instantiate(prefabData.laserTracker).GetComponent<LaserTracker>();
            t.laser = this;
            t.transform.parent = this.transform;
            t.rb.bodyType = RigidbodyType2D.Static;
            t.rb.simulated = false;
            t.transform.position = points[0].transform.position;
            t.end = t.transform.position;
            t.start = points[0].start;
            points[0].start = t.end;
            points[numBounces] = t;

            numBounces--;
        }
        else
        {
            points[0].rb.bodyType = RigidbodyType2D.Static;
            points[0].rb.simulated = false;
        }
    }
}
