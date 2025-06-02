using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public PlayerCard[] cards;


    // Start is called before the first frame update
    void Start()
    {
        cards = new PlayerCard[GameManager.instance.horses.Length];
    }

    // Update is called once per frame
    void Update()
    {
        /*
        for (int i = 0; i < cards.Length; i++)
        {
            Vector3 cardPos = new Vector3(-75, 220, 3);
            cardPos.y -= i * 60;
            cards[i].gameObject.transform.localPosition = cardPos;
        }*/
    }
}
