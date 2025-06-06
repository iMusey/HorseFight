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
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
}
