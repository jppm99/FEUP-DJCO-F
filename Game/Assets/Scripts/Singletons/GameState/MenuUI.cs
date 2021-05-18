using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    private enum MenuContext {
        StartMenu,
        GameMenu,
        EndMenu
    }

    private MenuContext context;
    private bool menuIsEnabled;
    public GameObject mainMenuUi;
    public GameObject gameMenu;
    public GameObject endGameMenu;

    void Start()
    {
        context = MenuContext.StartMenu;
        menuIsEnabled = true;
        Debug.Log("Script start");
        menuContextSettings();
        mainMenuUi.SetActive(true);
        gameMenu.SetActive(false);
        endGameMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Script updating");
        if (context == MenuContext.StartMenu || context == MenuContext.EndMenu)
        {
            menuContextSettings();
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && context == MenuContext.GameMenu)
            menuIsEnabled = !menuIsEnabled;

        if(menuIsEnabled)
        {
            menuContextSettings();
        }
        else
        {
            play();
        }
        
    }

    private void menuContextSettings()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void play()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void newGame()
    {
        play();
        gameStarted();
        menuIsEnabled = false;
        mainMenuUi.SetActive(false);
        RuntimeStuff.GetSingleton<Inventory>().NewGame();
    }

    public void gameStarted()
    {
        context = MenuContext.GameMenu;
    }

    public void gameEnded()
    {
        context = MenuContext.EndMenu;
    }
}
