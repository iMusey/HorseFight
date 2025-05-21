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
    public Vector3 rotation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        rotation = Vector3.zero;
        rotation.z = Mathf.Rad2Deg * Mathf.Atan2(rb.velocity.y, rb.velocity.x);
        transform.eulerAngles = rotation;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        laser.Bounce(transform.eulerAngles);
    }
}
