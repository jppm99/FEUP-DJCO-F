using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    private enum MenuContext {
        StartMenu,
        GameMenu,
        EndMenu
    }

    private enum MenuIds : int
    {
        MainMenu,
        GameMenu,
        InstructionsMenu,
        VictoryMenu,
        DefeatMenu
    }

    private MenuContext context;
    private bool menuIsEnabled;
    private bool showingInstructions;
    public GameObject mainMenuUi;
    public GameObject gameMenu;
    public GameObject instructionsMenu;
    public GameObject GameOverMenu;
    public GameObject GameWonMenu;

    private BackGroundController backgroundcontroller;

    private GameObject[] menus;

    void Start()
    {
        menus = new GameObject[5];
        menus[0] = mainMenuUi;
        menus[1] = gameMenu;
        menus[2] = instructionsMenu;
        menus[3] = GameWonMenu; 
         menus[4] = GameOverMenu;

        context = MenuContext.StartMenu;
        menuIsEnabled = true;
        showingInstructions = false;

        menuContextSettings();
        backgroundcontroller = GameObject.Find("BackGroundCanvas").GetComponent<BackGroundController>();
        backgroundcontroller.showMainMenu();
        showMainMenu();
    }

    public void disableAllMenus()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }
    }
    private void activateOneDisableRest(int index)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(i == index);
        }
    }

    private void showMainMenu()
    {
        activateOneDisableRest((int)MenuIds.MainMenu);
    }

    private void showGameMenu()
    {
        activateOneDisableRest((int)MenuIds.GameMenu);
    }
    private void showInstructionsMenu()
    {
        activateOneDisableRest((int)MenuIds.InstructionsMenu);
    }
    private void showVictoryMenu()
    {
        activateOneDisableRest((int)MenuIds.VictoryMenu);
    }
    private void showDefeatMenu()
    {
        activateOneDisableRest((int)MenuIds.DefeatMenu);
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

        if (Input.GetKeyDown(KeyCode.Escape) && context == MenuContext.GameMenu && !showingInstructions)
            menuIsEnabled = !menuIsEnabled;

        if(menuIsEnabled && menuIsEnabled != oldMenuState)
        {
            menuContextSettings();
            showGameMenu();
            backgroundcontroller.showMidGameMenu();
        }
        else if(menuIsEnabled != oldMenuState)
        {
            play();
            disableAllMenus();
        }
    }

    private void menuContextSettings()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void gameStarted()
    {
        context = MenuContext.GameMenu;
    }

    public void gameEnded()
    {
        context = MenuContext.EndMenu;
    }

    public void play()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameStarted();
        backgroundcontroller.disableAll();
        disableAllMenus();
    }

    /* BUTTONS FUNCTIONS */

    public void resumeGame()
    {
        play();
        disableAllMenus();
        menuIsEnabled = false;
    }

    public void newGame()
    {
        play();
        // inventory call must be first
        RuntimeStuff.GetSingleton<Inventory>().NewGame();
        RuntimeStuff.GetSingleton<GameManager>().ApplyState(false);
        menuIsEnabled = false;
    }

    public void loadGame()
    {
        play();
        RuntimeStuff.GetSingleton<Inventory>().LoadData();
        RuntimeStuff.GetSingleton<GameManager>().ApplyState(true);
        menuIsEnabled = false;
    }

    public void saveGame()
    {
        RuntimeStuff.GetSingleton<Inventory>().SaveData();
        RuntimeStuff.GetSingleton<GameState>().SaveData();
    }

    public void showInstructions()
    {
        if(menuIsEnabled && context == MenuContext.GameMenu)
        {
            showingInstructions = true;
            backgroundcontroller.showInstructions();
            showInstructionsMenu();
        }
        else if(context == MenuContext.StartMenu)
        {
            backgroundcontroller.showInstructions();
            showInstructionsMenu();
        }
    }

    public void closeInstructions()
    {
        if(showingInstructions && context == MenuContext.GameMenu)
        {
            showingInstructions = false;
            backgroundcontroller.showMidGameMenu();
            showGameMenu();
        }
        else if(context == MenuContext.StartMenu)
        {
            instructionsMenu.SetActive(false);
            backgroundcontroller.showMainMenu();
            showMainMenu();
        }
    }

    public void moveToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        // context = MenuContext.StartMenu;
        // menuContextSettings();
        // menuIsEnabled = false;
        // showMainMenu();
        // backgroundcontroller.showMainMenu();
    }

    public void playerHasWon()
    {
        Time.timeScale = 0;
        context = MenuContext.EndMenu;
        menuContextSettings();
        showVictoryMenu();
        backgroundcontroller.showWonGameMenu();
    }

    public void playerHasLost()
    {
        Time.timeScale = 0;
        context = MenuContext.EndMenu;
        menuContextSettings();
        showDefeatMenu();
        backgroundcontroller.showLostGameMenu();
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
