using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    public GameObject mainMenuBackGround;
    public GameObject midGameMenuBackGround;
    public GameObject endGameMenuBackGround;
    public GameObject instructionsBackground;


    public void disableAll()
    {
        mainMenuBackGround.SetActive(false);
        midGameMenuBackGround.SetActive(false);
        endGameMenuBackGround.SetActive(false);
        instructionsBackground.SetActive(false);
        instructionsBackground.SetActive(false);
    }

    public void showMainMenu()
    {
        mainMenuBackGround.SetActive(true);
        midGameMenuBackGround.SetActive(false);
        endGameMenuBackGround.SetActive(false);
        instructionsBackground.SetActive(false);
    }

    public void showMidGameMenu()
    {
        mainMenuBackGround.SetActive(false);
        midGameMenuBackGround.SetActive(true);
        endGameMenuBackGround.SetActive(false);
        instructionsBackground.SetActive(false);
    }

    public void showEndGameMenu()
    {
        mainMenuBackGround.SetActive(false);
        midGameMenuBackGround.SetActive(false);
        endGameMenuBackGround.SetActive(true);
        instructionsBackground.SetActive(false);
    }

    public void showInstructions()
    {
        mainMenuBackGround.SetActive(false);
        midGameMenuBackGround.SetActive(false);
        endGameMenuBackGround.SetActive(false);
        instructionsBackground.SetActive(true);
    }
}
