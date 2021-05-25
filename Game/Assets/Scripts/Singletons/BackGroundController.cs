using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    public GameObject mainMenuBackGround;
    public GameObject midGameMenuBackGround;
    public GameObject instructionsBackground;
    public GameObject gameLostBackground;
    public GameObject gameWonBackground;

    private GameObject[] backgrounds;

    private enum MenuContext : int
    {
        MainMenu,
        MidGameMenu,
        InstructionsGameMenu,
        GameLostMenu,
        GameWonMenu
    }

    void Start()
    {
        backgrounds = new GameObject[5];
        backgrounds[0] = mainMenuBackGround;
        backgrounds[1] = midGameMenuBackGround;
        backgrounds[2] = instructionsBackground;
        backgrounds[3] = gameLostBackground;
        backgrounds[4] = gameWonBackground;
    }

    public void disableAll()
    {
        for(int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(false);
        }
    }

    private void activateOneDisableRest(int index)
    {
        for(int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(i == index);
        }
    }

    public void showMainMenu()
    {
        activateOneDisableRest((int)MenuContext.MainMenu);
    }

    public void showMidGameMenu()
    {
        activateOneDisableRest((int)MenuContext.MidGameMenu);
    }

    public void showWonGameMenu()
    {
        activateOneDisableRest((int)MenuContext.GameWonMenu);
    }

    public void showLostGameMenu()
    {
        activateOneDisableRest((int)MenuContext.GameLostMenu);
    }

    public void showInstructions()
    {
        activateOneDisableRest((int)MenuContext.InstructionsGameMenu);
    }
}
