using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject MapSelectMenu;
    public GameObject CharacterSelectMenu;
    public GameObject GameStage;
    public GameObject PauseMenu;

    public GameObject currentMenu;

    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // press escape to pause
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            PauseMenu.SetActive(true);
            paused = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            PauseMenu.SetActive(false);
            paused = false;
            Time.timeScale = 1;
        }
    }

    public void ChangeMenu(GameObject menu)
    {
        currentMenu.SetActive(false);
        currentMenu = menu;
        currentMenu.SetActive(true);
        if (currentMenu.Equals(GameStage))
        {
            GameManager.instance.StartRound();
        }
    }
}
