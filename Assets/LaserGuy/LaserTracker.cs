using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class LaserTracker : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject beamSegment;
    public LaserScript laser;
    public float beamPercent;
    public Vector3 start;
    public Vector3 end;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude != 0)
        {
            end = transform.position + laser.transform.position;
        }
        PositionBeam(start, end);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        laser.Bounce();
    }

    public void PositionBeam(Vector3 start, Vector3 end)
    {
        // calculate new start based on beam percent
        Vector3 newStart = end - start;
        float newBeamLength = newStart.magnitude * beamPercent;
        newStart = newStart.normalized * newBeamLength;

        Vector3 center = (newStart + end) / 2;
        Vector3 direction = (end - newStart).normalized;

        beamSegment.transform.localScale = new Vector3(1f, 1 * newBeamLength, 1f);
        beamSegment.transform.localPosition = center - transform.position - laser.transform.position;
        beamSegment.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }
}
