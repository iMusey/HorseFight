using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JockeyScript : MonoBehaviour
{
    public float baseSpeed;
    public float maxHealth;
    public float strength;
    public float critChance;
    public float cooldownReduction;

    public string jockeyName;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Hit(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HorseScript>() != null)
        {
            Debug.Log("merp");
        }
    }
}
