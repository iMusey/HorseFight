using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public GameObject tracker;
    public LaserTracker[] points;
    public int numBounces;
    public float duration;
    public float speed;
    public Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        points = new LaserTracker[numBounces + 1];

        LaserTracker t = Instantiate(tracker).GetComponent<LaserTracker>();
        t.transform.position = transform.position;
        t.laser = this;
        t.rb.velocity = dir.normalized * speed;
        t.transform.parent = this.transform;

        points[0] = t;
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;

        if (duration <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Bounce(Vector3 rot)
    {
        if (numBounces > 0)
        {
            LaserTracker t = Instantiate(tracker).GetComponent<LaserTracker>();
            t.laser = this;
            t.transform.parent = this.transform;
            t.rb.bodyType = RigidbodyType2D.Static;
            t.rb.simulated = false;
            t.transform.position = points[0].transform.position;
            t.transform.eulerAngles = rot;
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
