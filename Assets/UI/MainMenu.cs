using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
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
        menuManager.ChangeMenu(menuManager.MapSelectMenu);
    }
    public void Other()
    {
        Debug.Log("Other");
    }
    public void Compendium()
    {
        Debug.Log("Compendium");
    }
    public void Settings()
    {
        Debug.Log("Settings");
    }
}
