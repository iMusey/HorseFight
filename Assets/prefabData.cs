using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PrefabData")]
public class prefabData : ScriptableObject
{
    public GameObject wreck;
    public GameObject scrap;
    public GameObject dmgNum;
    public GameObject card;
    public GameObject explosion;
    public GameObject laser;
    public GameObject laserTracker;

    public GameObject[] maps;
    public GameObject[] horses;
    public GameObject[] jockeys;

    public Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
