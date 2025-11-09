using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectMenu : MonoBehaviour
{
    public MenuManager menuManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        Debug.Log("Play");
        menuManager.ChangeMenu(menuManager.CharacterSelectMenu);
    }
}
