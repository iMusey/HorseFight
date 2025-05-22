using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    public bool devMode;

    public float time;
    public float timeSpeedFactor;

    public int[] horses;
    public int[] jockeys;


    public GameObject card;

    public MapScript map;
    public Leaderboard leaderboard;

    public int numPlayers;

    public prefabData prefabData;

    public static GameManager instance;


    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (devMode)
        {
            time = 120;
        }

        for (int i = 0; i < horses.Length; i++)
        {
            HorseScript horse = Instantiate(prefabData.horses[horses[i]]).GetComponent<HorseScript>();
            JockeyScript jockey = Instantiate(prefabData.jockeys[jockeys[i]]).GetComponent<JockeyScript>();
            jockey.transform.parent = horse.transform;
            jockey.HorseScript = horse;

            // jockey stat adjustments
            horse.speed += jockey.speed;
            horse.maxHealth += jockey.maxHealth;
            horse.health = horse.maxHealth;
            horse.strength += jockey.strength;
            horse.critChance += jockey.critChance;
            horse.cooldownReduction += jockey.cooldownReduction;

            // spawn data
            horse.movement.facing = map.spawns[i].facing;
            horse.gameObject.transform.position = map.spawns[i].transform.position;

            // name
            horse.transform.name = jockey.gameObject.transform.name + " " + horse.gameObject.transform.name;

            numPlayers++;
        }



        /*
        for (int i = 0; i < players.Length; i++)
        {
            // Make horse
            HorseInfoScript horseInfo = Instantiate(players[i]).GetComponent<HorseInfoScript>();
            HorseScript horse = Instantiate(horseInfo.horse).GetComponent<HorseScript>();

            if (devMode)
            {
                horse.maxHealth = horse.maxHealth / 3;
                horse.speed = horse.speed * 2;
            }

            // HORSE spawn data
            horse.facing = map.spawns[i].facing;
            horse.gameObject.transform.position = map.spawns[i].transform.position;
            horse.sprite.sprite = horseInfo.sprite;
            horse.bounceHit = horseInfo.hitSound;
            

            // make card
            PlayerCard pCard = Instantiate(card, leaderboard.transform).GetComponent<PlayerCard>();
            //pCard.title.text = jockey.jockeyName + " " + horseInfo.horseName;
            pCard.horse = horse;
            //pCard.icon.sprite = jockey.sprite;

            leaderboard.cards[i] = pCard;

            numPlayers++;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        // calculate time speed factor (make horses faster as time goes on)
        timeSpeedFactor = Mathf.Pow(time, 2) / 5000 + 1;
    }
}
