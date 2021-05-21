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

    public GameObject healthBar;
    public GameObject sanityBar;

    private BackGroundController backgroundcontroller; 

    void Start()
    {
        context = MenuContext.StartMenu;
        menuIsEnabled = true;
        menuContextSettings();
        mainMenuUi.SetActive(true);
        gameMenu.SetActive(false);
        endGameMenu.SetActive(false);

        backgroundcontroller = GameObject.Find("BackGroundCanvas").GetComponent<BackGroundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (context == MenuContext.StartMenu || context == MenuContext.EndMenu)
        {
            menuContextSettings();
            return;
        }

        bool oldMenuState = menuIsEnabled;

        if (Input.GetKeyDown(KeyCode.Escape) && context == MenuContext.GameMenu)
            menuIsEnabled = !menuIsEnabled;

        if(menuIsEnabled && menuIsEnabled != oldMenuState)
        {
            menuContextSettings();
            gameMenu.SetActive(true);
            backgroundcontroller.showMidGameMenu();
        }
        else if(menuIsEnabled != oldMenuState)
        {
            play();
            gameMenu.SetActive(false);
        }
    }

    private void menuContextSettings()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        healthBar.SetActive(false);
        sanityBar.SetActive(false);
    }

    public void resumeGame()
    {
        play();
        gameMenu.SetActive(false);
        menuIsEnabled = false;
    }

    public void play()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameStarted();
        healthBar.SetActive(true);
        sanityBar.SetActive(true);
        backgroundcontroller.disableAll();
    }

    public void newGame()
    {
        RuntimeStuff.GetSingleton<GameManager>().ApplyState(false);
        RuntimeStuff.GetSingleton<Inventory>().NewGame();
        play();
        menuIsEnabled = false;
        mainMenuUi.SetActive(false);
    }

    public void loadGame()
    {
        play();
        menuIsEnabled = false;
        mainMenuUi.SetActive(false);
        Debug.Log("Load objects missing");
    }

    public void saveGame()
    {
        RuntimeStuff.GetSingleton<Inventory>().SaveData();
        RuntimeStuff.GetSingleton<GameState>().SaveData();
        Debug.Log("Other objects missing");
    }

    public void moveToMainMenu()
    {
        context = MenuContext.StartMenu;
        menuContextSettings();
        mainMenuUi.SetActive(true);
        gameMenu.SetActive(false);
        endGameMenu.SetActive(false);
        menuIsEnabled = false;
        backgroundcontroller.showMainMenu();
    }

    public void gameStarted()
    {
        context = MenuContext.GameMenu;
    }

    public void gameEnded()
    {
        context = MenuContext.EndMenu;
    }

    public void quitGame()
    {
        /*
         * Shut down the running application. The Application.Quit call is ignored in the Editor.
         * https://docs.unity3d.com/ScriptReference/Application.Quit.html
         */
        Application.Quit();
    }
}
