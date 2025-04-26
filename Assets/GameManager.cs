using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public float time;
    public float timeSpeedFactor;

    public GameObject[] players;

    public MapScript map;

    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        for (int i = 0; i < players.Length; i++)
        {
            if (i == 1)
            {
                GameObject p = Instantiate(players[i]);
                p.GetComponent<HorseScript>().facing = map.spawns[i].facing;
                p.transform.position = map.spawns[i].transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        // calculate time speed factor (make horses faster as time goes on)
        timeSpeedFactor = Mathf.Pow(time, 2) / 7500 + 1;
    }
}
