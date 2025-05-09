using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public bool devMode;

    public float time;
    public float timeSpeedFactor;

    public GameObject[] players;
    public GameObject card;

    public MapScript map;
    public Leaderboard leaderboard;

    public int numPlayers;

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
            time = 90;
        }

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

            // set spawn data
            horse.facing = map.spawns[i].facing;
            horse.gameObject.transform.position = map.spawns[i].transform.position;
            horse.sprite.sprite = horseInfo.sprite;
            horse.bounceHit = horseInfo.hitSound;

            // make card
            PlayerCard pCard = Instantiate(card, leaderboard.transform).GetComponent<PlayerCard>();
            pCard.title.text = horseInfo.horseName;
            pCard.horse = horse;
            pCard.icon.sprite = horseInfo.sprite;

            leaderboard.cards[i] = pCard;

            numPlayers++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        // calculate time speed factor (make horses faster as time goes on)
        timeSpeedFactor = Mathf.Pow(time, 2) / 5000 + 1;
    }
}
