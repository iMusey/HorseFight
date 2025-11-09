using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    public bool devMode;

    public float time;
    public float timeSpeedFactor;
    public int numPlayers;

    // Chosen Indexs
    public int[] horses;
    public int[] jockeys;
    public int mapChoice;

    public MapScript map;
    public Leaderboard leaderboard;

    public prefabData prefabData;

    public static GameManager instance;


    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        // calculate time speed factor (make horses faster as time goes on)
        timeSpeedFactor = Mathf.Pow(time, 2) / 5000 + 1;
    }

    public void StartRound()
    {
        map = Instantiate(prefabData.maps[mapChoice]).GetComponent<MapScript>();


        if (devMode)
        {
            time = 120;
        }

        leaderboard.cards = new PlayerCard[GameManager.instance.horses.Length];

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
            horse.armor += jockey.armor;
            horse.strength += jockey.strength;
            horse.critChance += jockey.critChance;
            horse.cooldownReduction += jockey.cooldownReduction;


            // spawn data
            horse.movement.facing = map.spawns[i].facing;
            horse.gameObject.transform.position = map.spawns[i].transform.position;


            // make card
            PlayerCard pCard = Instantiate(prefabData.card, leaderboard.transform).GetComponent<PlayerCard>();
            pCard.title.text = (jockey.jockeyName + " " + horse.horseName);
            pCard.horse = horse;
            pCard.icon.sprite = jockey.sprite;
            leaderboard.cards[i] = pCard;



            numPlayers++;
        }
    }
}
