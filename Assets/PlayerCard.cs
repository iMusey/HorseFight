using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Slider slider;
    public HorseScript horse;
    public JockeyScript jockey;
    public Image icon;


    public bool live;
    public Image dead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = horse.hpP;

        if (horse.hpP <= 0)
        {
            live = false;
        }

        // grey out when dead
        if (!live)
        {
            Color c = Color.black;
            c.a = 0.5f;
            dead.color = c;
        }
    }
}
